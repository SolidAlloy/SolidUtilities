namespace SolidUtilities.UnityEditorInternals
{
    public static class LogEntry
    {
        public static void Internal_RemoveLogEntriesByMode(int mode) => UnityEditor.LogEntry.RemoveLogEntriesByMode(mode);
    }
}