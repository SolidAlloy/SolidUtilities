namespace SolidUtilities.Editor.Extensions
{
    using JetBrains.Annotations;
    using UnityEditor;
    using UnityEngine;

    public static class RectExtensions
    {
        [PublicAPI]
        public static Rect ShiftLinesDown(this Rect rect, int linesNum)
        {
            const float paddingBetweenFields = 2f;
            const float indentPerLevel = 15f;

            rect.xMin += linesNum * indentPerLevel;
            rect.y += EditorGUIUtility.singleLineHeight + paddingBetweenFields;

            return rect;
        }
    }
}