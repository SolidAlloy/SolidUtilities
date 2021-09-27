namespace SolidUtilities.UnityEditorInternals
{
    using UnityEditor;

    public static class MonoImporterProxy
    {
        public static void CopyMonoScriptIconToImporters(MonoScript script) => MonoImporter.CopyMonoScriptIconToImporters(script);
    }
}