namespace SolidUtilities.UnityEditorInternals
{
    using UnityEditor;

    public static class ConsoleWindowProxy
    {
        public static string StacktraceWithHyperlinks(string stackTrace, int startFrom)
        {
            return ConsoleWindow.StacktraceWithHyperlinks(stackTrace, startFrom);
        }
    }
}