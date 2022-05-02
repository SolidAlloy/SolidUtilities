namespace SolidUtilities.Editor
{
    using System.Collections.Generic;
    using UnityEditor;
    using UnityEditor.SceneManagement;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public static class ProjectWideSearcher
    {
        public static IEnumerable<SerializedObject> GetSerializedObjectsInProject()
        {
            foreach (var serializedObject in GetSerializedObjectsFromScenes())
            {
                yield return serializedObject;
            }

            foreach (var serializedObject in GetSerializedObjectsFromPrefabs())
            {
                yield return serializedObject;
            }

            foreach (var serializedObject in GetSerializedObjectsFromScriptableObjects())
            {
                yield return serializedObject;
            }
        }

        private static IEnumerable<SerializedObject> GetSerializedObjectsFromScenes()
        {
            var sceneGUIDs = AssetDatabase.FindAssets("t:scene", new[] { "Assets" });

            foreach (string sceneGUID in sceneGUIDs)
            {
                string scenePath = AssetDatabase.GUIDToAssetPath(sceneGUID);

                var currentScene = SceneManager.GetActiveScene();

                var scene = currentScene.path == scenePath ? currentScene : EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Additive);

                var rootGameObjects = scene.GetRootGameObjects();

                foreach (GameObject rootGameObject in rootGameObjects)
                {
                    if (rootGameObject == null)
                        continue;

                    foreach (var serializedObject in GetSerializedObjectsFromGameObject(rootGameObject))
                    {
                        yield return serializedObject;
                    }
                }

                if (scene != currentScene)
                    EditorSceneManager.CloseScene(scene, true);
            }
        }

        private static IEnumerable<SerializedObject> GetSerializedObjectsFromGameObject(GameObject gameObject)
        {
            var components = gameObject.GetComponentsInChildren<Component>();

            foreach (Component component in components)
            {
                if (component == null)
                    continue;

                var serializedObject = new SerializedObject(component);
                yield return serializedObject;
            }
        }

        private static IEnumerable<SerializedObject> GetSerializedObjectsFromPrefabs()
        {
            var prefabGUIDs = AssetDatabase.FindAssets("t:prefab");

            foreach (var prefabGUID in prefabGUIDs)
            {
                string prefabPath = AssetDatabase.GUIDToAssetPath(prefabGUID);

                if (string.IsNullOrEmpty(prefabPath))
                    continue;

                var rootGameObject = PrefabUtility.LoadPrefabContents(prefabPath);

                if (rootGameObject == null)
                    continue;

                foreach (var serializedObject in GetSerializedObjectsFromGameObject(rootGameObject))
                {
                    yield return serializedObject;
                }

                PrefabUtility.UnloadPrefabContents(rootGameObject);
            }
        }

        private static IEnumerable<SerializedObject> GetSerializedObjectsFromScriptableObjects()
        {
            var soGUIDs = AssetDatabase.FindAssets("t:ScriptableObject");

            foreach (string soGUID in soGUIDs)
            {
                string soPath = AssetDatabase.AssetPathToGUID(soGUID);

                var scriptableObject = AssetDatabase.LoadAssetAtPath<ScriptableObject>(soPath);

                if (scriptableObject == null)
                    continue;

                yield return new SerializedObject(scriptableObject);
                Resources.UnloadAsset(scriptableObject);
            }
        }
    }
}