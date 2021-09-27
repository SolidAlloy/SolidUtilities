namespace SolidUtilities.UnityEditorInternals
{
    using UnityEditor;

    public static class ProjectWindowUtilProxy
    {
        public static string GetActiveFolderPath() => ProjectWindowUtil.GetActiveFolderPath();
    }
}