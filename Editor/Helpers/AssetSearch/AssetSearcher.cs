namespace SolidUtilities.Editor.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text.RegularExpressions;
    using Extensions;
    using JetBrains.Annotations;
    using SolidUtilities.Extensions;
    using SolidUtilities.Helpers;
    using UnityEditor;
    using UnityEngine.Assertions;

    public enum ObjectType { ScriptableObject, Prefab, SceneObject, PrefabOverride }

    /// <summary>
    /// A class that allows to find assets provided different parameters.
    /// </summary>
    public static class AssetSearcher
    {
        private const string Asset = "Asset";
        private const string Scene = "Scene";
        private const string Component = "Component";
        private const string Object = "Object";

        private const string ScriptLinePattern = "m_Script: ";

        private static readonly Regex GUIDRegex = new Regex(@"(?<=guid:[\s]+)\w+?(?=,)", RegexOptions.Compiled);
        private static readonly Regex ObjectNameRegex = new Regex(@"(?<=m_Name:[\s]+).+?$", RegexOptions.Compiled);
        private static readonly Regex ObjectNamePrefabRegex = new Regex(@"(?<=value:[\s]+).+?$", RegexOptions.Compiled);
        private static readonly Regex PrefabFileIdRegex = new Regex(@"(?<=fileID:[\s]+)\d+?(?=,)", RegexOptions.Compiled);

        /// <summary>
        /// Finds all scriptable objects, scene objects, prefabs, and their overrides that contain a variable named
        /// <paramref name="variableName"/> with value equal to <paramref name="value"/>.
        /// </summary>
        /// <param name="variableName">The name of the variable to search for.</param>
        /// <param name="value">The value of the variable to search for.</param>
        /// <returns>A list of <see cref="FoundObject"/> that contain details about each found match.</returns>
        /// <example><code>
        /// [Serializable] public class CustomClass
        /// {
        ///     [SerializeField] private string _testField;
        /// }
        ///
        /// public class TestBehaviour : MonoBehaviour
        /// {
        ///     [SerializeField] private CustomClass _test;
        /// }
        ///
        /// public class TestScriptableObject : ScriptableObject
        /// {
        ///     public CustomClass ScriptableTestField;
        /// }
        ///
        /// // This will find all prefabs, scriptable objects, and scene objects where the value of _testField was set
        /// // to "value set in editor". It will then output details about each found match: path to the asset, component
        /// // where the variable was found, etc.
        /// var foundObjects = AssetSearcher.FindObjectsWithValue("_testField", "value set in editor");
        ///
        /// foreach (FoundObject foundObject in foundObjects)
        /// {
        ///     Debug.Log($"[foundObject.Type]");
        ///
        ///     foreach (var detail in foundObject)
        ///     {
        ///         Debug.Log($"{detail.Key}: {detail.Value}");
        ///     }
        /// }
        /// </code></example>
        [PublicAPI]
        public static List<FoundObject> FindObjectsWithValue(string variableName, string value)
        {
            var foundObjects = new List<FoundObject>();

            var guids = AssetDatabase.FindAssets("t:Prefab t:ScriptableObject t:Scene");

            foreach (string guid in guids)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(guid);

                if (assetPath.Contains("com.unity"))
                    continue;

                foundObjects.AddRange(IsScene(assetPath)
                    ? FindObjectsOnScene(assetPath, variableName, value)
                    : FindObjectsInFile(assetPath, variableName, value));
            }

            return foundObjects;
        }

        /// <summary>
        /// Gets the GUID of an asset where the type is located.
        /// </summary>
        /// <param name="type">Type to search for in assets.</param>
        /// <param name="GUID">GUID of the asset where the type is located, or <c>null</c> if the asset was not found.</param>
        /// <param name="monoScript">MonoScript of the asset where the type is located, or <c>null</c> if the asset was not found.</param>
        /// <returns><c>true</c> if the asset with the specified type was found.</returns>
        [PublicAPI]
        [ContractAnnotation("=> true, GUID: notnull; => false, GUID: null")]
        public static bool GetAssetDetails(Type type, [CanBeNull] out string GUID, out MonoScript monoScript)
        {
            GUID = string.Empty;
            monoScript = null;

            if (type == null)
                return false;

            if (type.IsGenericType)
                type = type.GetGenericTypeDefinition();

            foreach (string guid in AssetDatabase.FindAssets($"t:MonoScript {type.Name.StripGenericSuffix()}"))
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(guid);
                var asset = AssetDatabase.LoadAssetAtPath<MonoScript>(assetPath);

                if (asset is null || asset.GetClassType() != type)
                    continue;

                GUID = guid;
                monoScript = asset;
                return true;
            }

            return false;
        }

        public static string GetClassGUID(Type type) =>
            GetAssetDetails(type, out string guid, out MonoScript _) ? guid : string.Empty;

        public static MonoScript GetMonoScriptFromType(Type type)
        {
            GetAssetDetails(type, out string _, out MonoScript monoScript);
            return monoScript;
        }

        private static List<FoundObject> FindObjectsInFile(string assetPath, string variableName,
            string value)
        {
            var foundObjects = new List<FoundObject>();

            string[] lines = File.ReadAllLines(assetPath);
            int linesLength = lines.Length;

            for (int i = 0; i < linesLength; i++)
            {
                string line = lines[i];

                if ( ! line.Contains($"{variableName}: {value}"))
                    continue;

                if (IsPrefab(assetPath))
                {
                    foundObjects.Add(GetPrefab(lines, i, assetPath));
                }
                else
                {
                    foundObjects.Add(GetScriptableObject(assetPath));
                    break;
                }
            }

            return foundObjects;
        }

        private static FoundObject GetPrefab(string[] lines, int index, string relativePath)
        {
            // "m_Script:" line is a part of MonoBehaviour in YAML representation of the asset. It is always
            // located above all the custom variables declared in the MonoBehaviour.
            // It contains GUID to the MonoBehaviour asset that we can use to get the component name.
            int scriptLineIndex = FindClosestLineAboveWithText(lines, index, ScriptLinePattern);
            string componentName = GetComponentNameFromScriptLine(lines[scriptLineIndex]);

            return new FoundObject(ObjectType.Prefab) { { Asset, relativePath }, { Component, componentName } };
        }

        private static FoundObject GetScriptableObject(string assetPath)
        {
            return new FoundObject(ObjectType.ScriptableObject) { { Asset, assetPath } };
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

        private static List<FoundObject> FindObjectsOnScene(string scenePath, string variableName, string value)
        {
            var foundObjects = new List<FoundObject>();

            string[] lines = File.ReadAllLines(scenePath);
            int linesLength = lines.Length;

            for (int index = 0; index < linesLength; index++)
            {
                string line = lines[index];

                // When value is overriden in a prefab, the lines looks like this:
                // propertyPath: _typeRef.TypeNameAndAssembly
                // value: ExtendedScriptableObjects.Variable`1, ExtendedScriptableObjects
                // So if the value was found, we also must check that it belongs to the correct variable
                // (in this case, TypeNameAndAssembly).
                if (line.Contains($"{variableName}: {value}"))
                {
                    foundObjects.Add(GetSceneObject(lines, index, scenePath));
                }
                else if (line.Contains($"value: {value}") && lines[index - 1].Contains(variableName))
                {
                    foundObjects.Add(GetPrefabOverride(lines, index, scenePath));
                }
            }

            return foundObjects;
        }

        private static FoundObject GetPrefabOverride(string[] lines, int valueLineIndex, string scenePath)
        {
            // For each prefab instance on a scene, there is a block of lines in the YAML representation of the scene.
            // All modifications (i.e. overriden values) are located in the m_Modifications block.
            // When a matching value was found, we find which m_Modifications block it is the part of.
            // Then, we can find propertyPath in the same Modifications block by searching the lines below its header.
            int modificationsLineIndex = FindClosestLineAboveWithText(lines, valueLineIndex, "m_Modifications:");

            // The object name variable is named m_Name and looks like this in the YAML representation on the scene:
            // propertyPath: m_Name
            // value: Default Prefab
            // So we first search for the m_Name line. Then, we can find the name of the object on the next line.
            int propertyPathLineIndex = FindClosestLineBelowWithText(lines, modificationsLineIndex, "propertyPath: m_Name");

            string objectNameLine = lines[propertyPathLineIndex + 1];
            string objectName = ObjectNamePrefabRegex.Find(objectNameLine);

            // The value block in the YAML representation of the scene looks like this:
            // - target: {fileID: 4272606278695419953, guid: 9727a72804665ad4bafc0cb82f431746, type: 3}
            // propertyPath: _typeRef.TypeNameAndAssembly
            // value: ExtendedScriptableObjects.Variable`1, ExtendedScriptableObjects
            // In the target line that is located 2 lines above the value, we can find GUID to the prefab asset and
            // FileID that is a locator of the component where the value was overriden.
            string targetLine = lines[valueLineIndex - 2];

            string prefabGuid = GUIDRegex.Find(targetLine);
            string componentFileID = PrefabFileIdRegex.Find(targetLine);

            string prefabPath = AssetDatabase.GUIDToAssetPath(prefabGuid);
            string[] prefabLines = File.ReadAllLines(prefabPath);

            // We just search for the component line from the start of the file. There is only one such line per file.
            int fileIdLineIndex = FindClosestLineBelowWithText(prefabLines, 0, $"--- !u!114 &{componentFileID}");

            // "m_Script:" line inside the YAML representation of the MonoBehaviour contains GUID of the MonoBehaviour
            // and will let us find the component name.
            int scriptLineIndex = FindClosestLineBelowWithText(prefabLines, fileIdLineIndex, ScriptLinePattern);
            string componentName = GetComponentNameFromScriptLine(prefabLines[scriptLineIndex]);

            return new FoundObject(ObjectType.PrefabOverride) { { Scene, scenePath }, { Object, objectName }, { Component, componentName } };
        }

        private static FoundObject GetSceneObject(string[] lines, int valueLineIndex, string scenePath)
        {
            // "m_Script:" line inside the YAML representation of the MonoBehaviour is always located above all the
            // custom variables. It contains GUID of the MonoBehaviour and will let us find the component name.
            int scriptLineIndex = FindClosestLineAboveWithText(lines, valueLineIndex, ScriptLinePattern);
            string componentName = GetComponentNameFromScriptLine(lines[scriptLineIndex]);

            // GameObject info block is always located above all the MonoBehaviours it contains, so if we search above
            // the MonoBehaviour where we found the variable, we will find the GameObject where this MonoBehaviour is
            // located.
            int gameObjectLineIndex = FindClosestLineAboveWithText(lines, scriptLineIndex, "GameObject:", true);

            // GameObject info block contains the m_Name variable that stores the name of the game object.
            int objectNameLineIndex = FindClosestLineBelowWithText(lines, gameObjectLineIndex, "m_Name:");
            string objectName = ObjectNameRegex.Find(lines[objectNameLineIndex]);

            return new FoundObject(ObjectType.SceneObject) { { Scene, scenePath }, { Object, objectName }, { Component, componentName } };
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