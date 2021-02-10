namespace SolidUtilities.UnityEditorInternals
{
    using JetBrains.Annotations;
    using UnityEditor;

    public static class SerializedPropertyExtensions
    {
        /// <summary> Checks if property has custom drawer. </summary>
        /// <param name="property">The property to check.</param>
        /// <returns> <c>true</c> if the <paramref name="property"/> has a custom drawer. </returns>
        [PublicAPI]
        public static bool HasCustomPropertyDrawer(this SerializedProperty property)
        {
            return ScriptAttributeUtility.GetHandler(property).propertyDrawer != null;
        }
    }
}