namespace SolidUtilities.Editor
{
    using JetBrains.Annotations;
    using UnityEditor;

    public static class DrawerReplacer
    {
        /// <summary>
        /// Replaces the default <see cref="PropertyDrawer"/> for <typeparamref name="TObject"/> with a custom one.
        /// </summary>
        /// <typeparam name="TObject">
        /// The type of object to use the custom <see cref="PropertyDrawer"/> for.
        /// </typeparam>
        /// <typeparam name="TDrawer">
        /// The type of custom <see cref="PropertyDrawer"/> to use for <typeparamref name="TObject"/>.
        /// </typeparam>
        /// <example><code>
        /// public class CustomUnityEventDrawer : PropertyDrawer
        /// {
        ///     [DidReloadScripts]
        ///     private static void ReplaceDefaultDrawer()
        ///         => DrawerReplacer.ReplaceDefaultDrawer&lt;UnityEventBase, CustomUnityEventDrawer>();
        ///
        ///     public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) { }
        /// }
        /// </code></example>
        [PublicAPI]
        public static void ReplaceDefaultDrawer<TObject, TDrawer>()
            where TDrawer : PropertyDrawer
        {
            UnityEditorInternals.DrawerReplacer.ReplaceDefaultDrawer<TObject, TDrawer>();
        }
    }
}