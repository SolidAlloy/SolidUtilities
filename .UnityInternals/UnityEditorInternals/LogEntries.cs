namespace SolidUtilities.UnityEditorInternals
{
    public static class LogEntries
    {
        public static int GetCount() => UnityEditor.LogEntries.GetCount();

        public static (int errorCount, int warningCount, int logCount) GetCountsByType()
        {
            int errorCount, warningCount, logCount;
            errorCount = warningCount = logCount = 0;

            UnityEditor.LogEntries.GetCountsByType(ref errorCount, ref warningCount, ref logCount);

            return (errorCount, warningCount, logCount);
        }

        public static void Clear() => UnityEditor.LogEntries.Clear();
    }
}