namespace SolidUtilities.UnityEditorInternals
{
    using JetBrains.Annotations;
    using UnityEditor;

    public static class DrawerReplacer
    {
        [PublicAPI]
        public static void ReplaceDefaultDrawer<TObject, TDrawer>()
            where TDrawer : PropertyDrawer
        {
            if (ScriptAttributeUtility.s_DrawerTypeForType == null)
            {
                ScriptAttributeUtility.BuildDrawerTypeForTypeDictionary();
            }

            var keySet = new ScriptAttributeUtility.DrawerKeySet
            {
                type = typeof(TObject),
                drawer = typeof(TDrawer)
            };

            ScriptAttributeUtility.s_DrawerTypeForType[typeof(TObject)] = keySet;
        }
    }
}