namespace SolidUtilities.UnityEditorInternals
{
    using UnityEditor;

    public static class MonoScriptExtensions
    {
        public static string Internal_GetAssemblyName(this MonoScript script) => script.GetAssemblyName();
    }
}