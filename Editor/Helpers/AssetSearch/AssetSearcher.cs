namespace SolidUtilities.Editor.Helpers.AssetSearch
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text.RegularExpressions;
    using Helpers;
    using JetBrains.Annotations;
    using SolidUtilities.Extensions;
    using UnityEditor;
    using UnityEngine;
    using UnityEngine.Assertions;

    public enum ObjectType { ScriptableObject, Prefab, SceneObject, PrefabOverride }

    public static class AssetSearcher
    {
        private const string AssetPath = "AssetPath";
        private const string ScenePath = "ScenePath";
        private const string Component = "Component";
        private const string Object = "Object";

        private static readonly Regex _guidRegex = new Regex(@"(?<=guid:[\s]+)\w+?(?=,)", RegexOptions.Compiled);
        private static readonly Regex _objectNameRegex = new Regex(@"(?<=m_Name:[\s]+).+?$", RegexOptions.Compiled);
        private static readonly Regex _objectNamePrefabRegex = new Regex(@"(?<=value:[\s]+).+?$", RegexOptions.Compiled);
        private static readonly Regex _prefabFileIdRegex = new Regex(@"(?<=fileID:[\s]+)\d+?(?=,)", RegexOptions.Compiled);

        [PublicAPI]
        public static List<FoundObject> FindObjectsWithValue(string variableName, string value)
        {
            var foundObjects = new List<FoundObject>();

            var guids = AssetDatabase.FindAssets("t:Prefab t:ScriptableObject t:Scene", new[] { "Assets" });

            foreach (string guid in guids)
            {
                string relativeAssetPath = AssetDatabase.GUIDToAssetPath(guid);
                string absolutePath = AssetDatabaseHelper.RelativeToAbsolutePath(relativeAssetPath);

                if (IsScene(absolutePath))
                {
                    foundObjects.AddRange(FindObjectsOnScene(absolutePath, relativeAssetPath, variableName, value));
                }
                else
                {
                    string[] lines = File.ReadAllLines(absolutePath);
                    int linesLength = lines.Length;

                    for (int i = 0; i < linesLength; i++)
                    {
                        string line = lines[i];
                        if (!line.Contains($"{variableName}: {value}"))
                            continue;

                        if (IsPrefab(absolutePath))
                        {
                            var foundObject = GetPrefab(lines, i, relativeAssetPath);
                            foundObjects.Add(foundObject);
                        }
                        else
                        {
                            var foundObject = GetScriptableObject(relativeAssetPath);
                            foundObjects.Add(foundObject);
                        }

                        break;
                    }
                }
            }

            return foundObjects;
        }

        private static FoundObject GetScriptableObject(string assetPath)
        {
            return new FoundObject(ObjectType.ScriptableObject) { { AssetPath, assetPath } };
        }

        private static bool IsScene([NotNull] string assetPath)
        {
            string lastChars = assetPath.Substring(assetPath.Length - 5);
            return lastChars == "unity";
        }

        private static bool IsPrefab([NotNull] string assetPath)
        {
            string lastChars = assetPath.Substring(assetPath.Length - 6);
            return lastChars == "prefab";
        }

        private static List<FoundObject> FindObjectsOnScene(string absolutePath, string relativePath, string variableName, string value)
        {
            var foundObjects = new List<FoundObject>();

            string[] lines = File.ReadAllLines(absolutePath);
            int linesLength = lines.Length;

            for (int i = 0; i < linesLength; i++)
            {
                string line = lines[i];

                if (line.Contains($"{variableName}: {value}"))
                {
                    foundObjects.Add(GetSceneObject(lines, i, relativePath));
                }
                else if (line.Contains($"value: {value}"))
                {
                    foundObjects.Add(GetPrefabOverride(lines, i, relativePath));
                }
            }

            return foundObjects;
        }

        private static FoundObject GetPrefab(string[] lines, int index, string relativePath)
        {
            int scriptLineIndex = FindClosestLineAboveWithText(lines, index, "m_Script: ");
            Assert.IsFalse(scriptLineIndex == -1);

            string componentGuid = _guidRegex.Find(lines[scriptLineIndex]);

            string componentPath = AssetDatabase.GUIDToAssetPath(componentGuid);
            string componentName = GetComponentName(componentPath);

            return new FoundObject(ObjectType.Prefab) { { AssetPath, relativePath }, { Component, componentName } };
        }

        private static FoundObject GetPrefabOverride(string[] lines, int index, string relativePath)
        {
            int propertyPathLineIndex = FindClosestLineBelowWithText(lines, index, "propertyPath: m_Name");
            Assert.IsFalse(propertyPathLineIndex == -1);

            string objectNameLine = lines[propertyPathLineIndex + 1];
            string objectName = _objectNamePrefabRegex.Find(objectNameLine);

            string targetLine = lines[index - 2];

            string prefabGuid = _guidRegex.Find(targetLine);
            string componentFileID = _prefabFileIdRegex.Find(targetLine);

            string prefabPath = AssetDatabaseHelper.GUIDToAbsolutePath(prefabGuid);
            string[] prefabLines = File.ReadAllLines(prefabPath);


            int fileIdLineIndex = FindClosestLineBelowWithText(prefabLines, 0, $"--- !u!114 &{componentFileID}");
            if (fileIdLineIndex == -1)
            {
                Debug.Log(prefabPath);
                Debug.Log($"--- !u!114 &{componentFileID}");
            }

            Assert.IsFalse(fileIdLineIndex == -1);

            int scriptLineIndex = FindClosestLineBelowWithText(prefabLines, fileIdLineIndex, "m_Script: ");
            Assert.IsFalse(scriptLineIndex == -1);

            string componentGuid = _guidRegex.Find(prefabLines[scriptLineIndex]);
            string componentPath = AssetDatabase.GUIDToAssetPath(componentGuid);
            string componentName = GetComponentName(componentPath);

            return new FoundObject(ObjectType.PrefabOverride) { { ScenePath, relativePath }, { Object, objectName }, { Component, componentName } };
        }

        private static FoundObject GetSceneObject(string[] lines, int index, string relativePath)
        {
            int scriptLineIndex = FindClosestLineAboveWithText(lines, index, "m_Script: ");
            Assert.IsFalse(scriptLineIndex == -1);

            string componentGuid = _guidRegex.Find(lines[scriptLineIndex]);

            string componentPath = AssetDatabase.GUIDToAssetPath(componentGuid);
            string componentName = GetComponentName(componentPath);

            int gameObjectLineIndex = FindClosestLineAboveWithText(lines, scriptLineIndex, "GameObject:", true);
            Assert.IsFalse(gameObjectLineIndex == -1);

            int objectNameLineIndex = FindClosestLineBelowWithText(lines, gameObjectLineIndex, "m_Name:");
            Assert.IsFalse(objectNameLineIndex == -1);

            string objectName = _objectNameRegex.Find(lines[objectNameLineIndex]);

            return new FoundObject(ObjectType.SceneObject) { { ScenePath, relativePath }, { Object, objectName }, { Component, componentName } };
        }

        private static string GetComponentName(string componentPath)
        {
            int charAfterLastSlashIndex = componentPath.LastIndexOf('/') + 1;
            int nameLength = componentPath.Length - charAfterLastSlashIndex - 3;
            string componentName = componentPath.Substring(charAfterLastSlashIndex, nameLength);
            string niceComponentName = ObjectNames.NicifyVariableName(componentName);
            return niceComponentName;
        }

        private static int FindClosestLineAboveWithText(string[] lines, int index, string text, bool exactMatch = false)
        {
            Assert.IsTrue(index >= 0);

            for (int j = index; j != 0; j--)
            {
                if (exactMatch)
                {
                    if (lines[j] == text)
                        return j;
                }
                else
                {
                    if (lines[j].Contains(text))
                        return j;
                }
            }

            return -1;
        }

        private static int FindClosestLineBelowWithText(string[] lines, int index, string text)
        {
            Assert.IsTrue(index >= 0);

            int linesLength = lines.Length;

            Assert.IsTrue(index < linesLength);

            for (int j = index; j != linesLength; j++)
            {
                try
                {
                    if (lines[j].Contains(text))
                        return j;
                }
                catch (IndexOutOfRangeException)
                {
                    Debug.Log(j);
                    throw;
                }
            }

            return -1;
        }
    }
}