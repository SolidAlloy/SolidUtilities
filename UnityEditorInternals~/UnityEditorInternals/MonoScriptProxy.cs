namespace SolidUtilities.UnityEditorInternals
{
    using UnityEditor;

    public static class MonoScriptProxy
    {
        public static void CopyMonoScriptIconToImporters(MonoScript script) => MonoImporter.CopyMonoScriptIconToImporters(script);
    }
}