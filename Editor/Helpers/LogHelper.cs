namespace SolidUtilities.Editor.Helpers
{
    using System;
    using System.Reflection;
    using JetBrains.Annotations;
    using UnityEditor;
    using UnityEngine;

    [Flags]
    [PublicAPI]
    public enum LogModes
    {
        /// <summary> All user-generated log entries. </summary>
        UserAll = 0,

        /// <summary> Editor-generated errors. </summary>
        EditorErrors = 1 << 1,

        /// <summary> User-generated red log entries (errors, exceptions, assertions). </summary>
        UserErrorsAndExceptions = 1 << 8,

        /// <summary> User and editor-generated warnings. </summary>
        UserAndEditorWarnings = 1 << 9,

        /// <summary> User and editor-generated info messages. </summary>
        UserAndEditorInfos = 1 << 10,

        /// <summary> Mode of the "No script asset for..." warning. </summary>
        NoScriptAssetWarning = 1 << 18,

        /// <summary> User-generated info messages. </summary>
        UserInfo = UserAndEditorInfos | 1 << 14 | 1 << 23, // it's unknown what 14 and 23 are exactly for.

        /// <summary> User-generated warnings. </summary>
        UserWarning = UserAndEditorWarnings | 1 << 14 | 1 << 23
    }

    /// <summary>
    /// Contains different methods that simplify or extend operations on log entries.
    /// </summary>
    public static class LogHelper
    {
        private static readonly MethodInfo RemoveLogEntriesByModeMethod;

        static LogHelper()
        {
            const string logEntryClassName = "UnityEditor.LogEntry";
            const string removeLogMethodName = "RemoveLogEntriesByMode";

            var editorAssembly = Assembly.GetAssembly(typeof(Editor));
            Type logEntryType = editorAssembly.GetType(logEntryClassName);
            RemoveLogEntriesByModeMethod = logEntryType.GetMethod(removeLogMethodName, BindingFlags.NonPublic | BindingFlags.Static);

            if (RemoveLogEntriesByModeMethod == null)
            {
                Debug.LogError($"Could not find the {logEntryClassName}.{removeLogMethodName}() method. " +
                               "Please submit an issue and specify your Unity version: https://github.com/SolidAlloy/SolidUtilities/issues/new");
            }
        }

        /// <summary> Removes log entries that match <paramref name="mode"/> from console. </summary>
        /// <param name="mode">Mode of the log entries to remove.</param>
        [PublicAPI]
        public static void RemoveLogEntriesByMode(LogModes mode)
        {
            RemoveLogEntriesByModeMethod?.Invoke(null, new object[] { mode });
        }
    }
}