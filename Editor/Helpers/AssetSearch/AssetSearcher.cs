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
    using UnityEngine.Assertions;

    public enum ObjectType { ScriptableObject, Prefab, SceneObject, PrefabOverride }

    public static class AssetSearcher
    {
        private const string AssetPath = "AssetPath";
        private const string ScenePath = "ScenePath";
        private const string Component = "Component";
        private const string Object = "Object";

        private const string ScriptLinePattern = "m_Script: ";

        private static readonly Regex GUIDRegex = new Regex(@"(?<=guid:[\s]+)\w+?(?=,)", RegexOptions.Compiled);
        private static readonly Regex ObjectNameRegex = new Regex(@"(?<=m_Name:[\s]+).+?$", RegexOptions.Compiled);
        private static readonly Regex ObjectNamePrefabRegex = new Regex(@"(?<=value:[\s]+).+?$", RegexOptions.Compiled);
        private static readonly Regex PrefabFileIdRegex = new Regex(@"(?<=fileID:[\s]+)\d+?(?=,)", RegexOptions.Compiled);

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
                    if (TryFindValueInFile(absolutePath, relativeAssetPath, variableName, value, out FoundObject foundObject))
                        foundObjects.Add(foundObject);
                }
            }

            return foundObjects;
        }

        private static bool TryFindValueInFile(string absolutePath, string relativeAssetPath, string variableName,
            string value, out FoundObject foundObject)
        {
            string[] lines = File.ReadAllLines(absolutePath);
            int linesLength = lines.Length;

            for (int i = 0; i < linesLength; i++)
            {
                string line = lines[i];

                if ( ! line.Contains($"{variableName}: {value}"))
                    continue;

                foundObject = IsPrefab(absolutePath)
                    ? GetPrefab(lines, i, relativeAssetPath)
                    : GetScriptableObject(relativeAssetPath);

                return true;
            }

            foundObject = default;
            return false;
        }

        private static FoundObject GetPrefab(string[] lines, int index, string relativePath)
        {
            int scriptLineIndex = FindClosestLineAboveWithText(lines, index, ScriptLinePattern);
            string componentName = GetComponentNameFromScriptLine(lines[scriptLineIndex]);

            return new FoundObject(ObjectType.Prefab) { { AssetPath, relativePath }, { Component, componentName } };
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

            for (int index = 0; index < linesLength; index++)
            {
                string line = lines[index];

                if (line.Contains($"{variableName}: {value}"))
                {
                    foundObjects.Add(GetSceneObject(lines, index, relativePath));
                }
                else if (line.Contains($"value: {value}"))
                {
                    foundObjects.Add(GetPrefabOverride(lines, index, relativePath));
                }
            }

            return foundObjects;
        }

        private static FoundObject GetPrefabOverride(string[] lines, int valueLineIndex, string relativePath)
        {
            int propertyPathLineIndex = FindClosestLineBelowWithText(lines, valueLineIndex, "propertyPath: m_Name");

            string objectNameLine = lines[propertyPathLineIndex + 1];
            string objectName = ObjectNamePrefabRegex.Find(objectNameLine);

            string targetLine = lines[valueLineIndex - 2];

            string prefabGuid = GUIDRegex.Find(targetLine);
            string componentFileID = PrefabFileIdRegex.Find(targetLine);

            string prefabPath = AssetDatabaseHelper.GUIDToAbsolutePath(prefabGuid);
            string[] prefabLines = File.ReadAllLines(prefabPath);

            int fileIdLineIndex = FindClosestLineBelowWithText(prefabLines, 0, $"--- !u!114 &{componentFileID}");

            int scriptLineIndex = FindClosestLineBelowWithText(prefabLines, fileIdLineIndex, ScriptLinePattern);
            string componentName = GetComponentNameFromScriptLine(prefabLines[scriptLineIndex]);

            return new FoundObject(ObjectType.PrefabOverride) { { ScenePath, relativePath }, { Object, objectName }, { Component, componentName } };
        }

        private static FoundObject GetSceneObject(string[] lines, int valueLineIndex, string relativePath)
        {
            int scriptLineIndex = FindClosestLineAboveWithText(lines, valueLineIndex, ScriptLinePattern);
            string componentName = GetComponentNameFromScriptLine(lines[scriptLineIndex]);

            int gameObjectLineIndex = FindClosestLineAboveWithText(lines, scriptLineIndex, "GameObject:", true);

            int objectNameLineIndex = FindClosestLineBelowWithText(lines, gameObjectLineIndex, "m_Name:");

            string objectName = ObjectNameRegex.Find(lines[objectNameLineIndex]);

            return new FoundObject(ObjectType.SceneObject) { { ScenePath, relativePath }, { Component, componentName }, { Object, objectName } };
        }

        private static int FindClosestLineAboveWithText(string[] lines, int index, string text, bool exactMatch = false)
        {
            Assert.IsTrue(index >= 0);

            for (int j = index; j != 0; j--)
            {
                bool matchCondition = exactMatch ? lines[j] == text : lines[j].Contains(text);

                if (matchCondition)
                    return j;
            }

            throw new Exception($"No line with index less than {index} was found with text \"{text}\"");
        }

        private static int FindClosestLineBelowWithText(string[] lines, int index, string text)
        {
            int linesLength = lines.Length;

            Assert.IsTrue(index >= 0 && index < linesLength);

            for (int j = index; j != linesLength; j++)
            {
                if (lines[j].Contains(text))
                    return j;
            }

            throw new Exception($"No line with index more than {index} was found with text \"{text}\"");
        }

        private static string GetComponentNameFromScriptLine(string scriptLine)
        {
            string componentGuid = GUIDRegex.Find(scriptLine);
            string scriptPath = AssetDatabase.GUIDToAssetPath(componentGuid);
            string scriptName = GetScriptNameWithoutExtension(scriptPath);
            string componentName = ObjectNames.NicifyVariableName(scriptName);
            return componentName;
        }

        private static string GetScriptNameWithoutExtension(string filePath)
        {
            int charAfterLastSlashIndex = filePath.LastIndexOf('/') + 1;
            int nameLength = filePath.Length - charAfterLastSlashIndex - 3;
            return filePath.Substring(charAfterLastSlashIndex, nameLength);
        }
    }
}