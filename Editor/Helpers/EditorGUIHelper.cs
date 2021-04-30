namespace SolidUtilities.Editor.Helpers
{
    using JetBrains.Annotations;

    public static class EditorGUIHelper
    {
        [PublicAPI]
        public static bool HasKeyboardFocus(int controlID)
            => UnityEditorInternals.EditorGUIHelper.HasKeyboardFocus(controlID);
    }
}