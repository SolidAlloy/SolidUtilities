namespace GenericScriptableArchitecture.Editor
{
    using System;
    using System.Collections.Generic;
    using JetBrains.Annotations;
    using UnityEditor;
    using UnityEngine;

    /// <summary>
    /// Utility that allows changes to MonoBehaviours persist into edit mode.
    /// </summary>
    public static class PlayModeSaver
    {
        private static readonly Dictionary<int, Dictionary<string, Action<Component>>> _saveActions =
            new Dictionary<int, Dictionary<string, Action<Component>>>();

        static PlayModeSaver()
        {
            EditorApplication.playModeStateChanged += OnPlayModeChanged;
        }

        /// <summary>
        /// Executes <paramref name="change"/> and saves the change this action causes to <paramref name="component"/> so that it persists into edit mode.
        /// </summary>
        /// <param name="component">Component that is changed.</param>
        /// <param name="fieldName">Field of the changed component. Use nameof(field) to get the name.</param>
        /// <param name="change">A change to the component's field. Use the component passed into the action instead of the component reference you have at the moment.</param>
        public static void SaveChange([NotNull] Component component, [NotNull] string fieldName, [NotNull] Action<Component> change)
        {
            change(component);

            if (!EditorApplication.isPlaying || PrefabUtility.IsPartOfPrefabAsset(component))
            {
                return;
            }

            int instanceId = component.GetInstanceID();

            if (!_saveActions.TryGetValue(instanceId, out var componentActions))
            {
                componentActions = new Dictionary<string, Action<Component>>();
                _saveActions.Add(instanceId, componentActions);
            }

            componentActions[fieldName] = change;
        }

        private static void OnPlayModeChanged(PlayModeStateChange stateChange)
        {
            if (stateChange != PlayModeStateChange.EnteredEditMode)
                return;

            foreach (var instanceActions in _saveActions)
            {
                var objectTarget = EditorUtility.InstanceIDToObject(instanceActions.Key);

                if (objectTarget == null)
                {
                    Debug.LogWarning($"Tried to restore changes in component with id {instanceActions.Key} but no component was found by this ID.");
                    continue;
                }

                var componentActions = instanceActions.Value;

                if (componentActions.Values.Count <= 0)
                    continue;

                var componentTarget = (Component) objectTarget;

                Undo.RecordObject(objectTarget, "Restored changes to component after play mode.");
                foreach (var saveAction in componentActions.Values)
                {
                    saveAction(componentTarget);
                }
            }
        }
    }
}