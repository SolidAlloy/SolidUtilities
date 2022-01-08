namespace SolidUtilities.Editor
{
    using JetBrains.Annotations;
    using UnityEditorInternals;

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
        /// <summary> Removes log entries that match <paramref name="mode"/> from console. </summary>
        /// <param name="mode">Mode of the log entries to remove.</param>
        [PublicAPI]
        public static void RemoveLogEntriesByMode(LogModes mode)
        {
            LogEntry.Internal_RemoveLogEntriesByMode((int) mode);
        }

        /// <summary> Returns the total number of log entries in the console. </summary>
        /// <returns> Total number of log entries in the console. </returns>
        [PublicAPI]
        public static int GetCount() => LogEntries.GetCount();

        /// <summary> Returns the number of errors in the console. </summary>
        /// <returns> The number of errors in the console. </returns>
        [PublicAPI]
        public static int GetErrorCount()
        {
            (int errorCount, int _, int _) = LogEntries.GetCountsByType();
            return errorCount;
        }

        /// <summary> Returns the number of warnings in the console. </summary>
        /// <returns> The number of warnings in the console. </returns>
        [PublicAPI]
        public static int GetWarningCount()
        {
            (int _, int warningCount, int _) = LogEntries.GetCountsByType();
            return warningCount;
        }

        /// <summary> Returns the number of info logs in the console. </summary>
        /// <returns> The number of info logs in the console. </returns>
        [PublicAPI]
        public static int GetLogCount()
        {
            (int _, int _, int logCount) = LogEntries.GetCountsByType();
            return logCount;
        }

        /// <summary> Returns the number of log entries in the console by type: error logs, warning logs, info logs. </summary>
        /// <returns> The number of log entries in the console by type: error logs, warning logs, info logs. </returns>
        [PublicAPI]
        public static (int errorCount, int warningCount, int logCount) GetCountByType()
            => LogEntries.GetCountsByType();

        /// <summary> Removes all logs from the console. </summary>
        [PublicAPI]
        public static void Clear() => LogEntries.Clear();
    }
}