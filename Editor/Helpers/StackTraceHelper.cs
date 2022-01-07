namespace SolidUtilities.Editor
{
    using System.Text.RegularExpressions;
    using UnityEditorInternals;

    public static class StackTraceHelper
    {
        public static string EnvironmentToUnityStyle(string stackTrace)
        {
            // Remove at at the start of lines
            stackTrace = Regex.Replace(stackTrace, @" +at ", string.Empty);

            // Remove the location part of the stack line if there is no reference to a file on disc
            stackTrace = Regex.Replace(stackTrace, @" \[0x00000\] in <.*?(?=\n|$)", string.Empty);

            // Replace [0x00000] in with (at
            stackTrace = Regex.Replace(stackTrace, @"\[0x\w+?\] in", "(at");

            // Add a closing parenthese
            stackTrace = Regex.Replace(stackTrace, @"(?<=:\d+) ", ")");

            return stackTrace;
        }

        public static string AddLinks(string unityStackTrace)
        {
            return ConsoleWindowProxy.StacktraceWithHyperlinks(unityStackTrace, 0);
        }
    }
}