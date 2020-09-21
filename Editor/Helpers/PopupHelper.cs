namespace SolidUtilities.Editor.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    public static class PopupHelper
    {
        private const char Separator = '/';
        private const int ScrollbarWidth = 12;

        /// <summary>This makes a little indent after the longest item so that it is readable.</summary>
        private const int RightIndent = 5;

        public static int CalculatePopupWidth(IEnumerable<string> items, GUIStyle style, int globalOffsetWidth, int indentWidth, bool flatTree)
        {
            if ( ! items.Any())
                return 0;

            int maxItemWidth = items
                .Select(item => GetItemWidth(item, style, indentWidth, flatTree))
                .Max();

            maxItemWidth += globalOffsetWidth + ScrollbarWidth + RightIndent;
            return maxItemWidth;
        }

        private static int GetItemWidth(string item, GUIStyle style, int indentWidth, bool flatTree)
        {
            if (flatTree)
                return GetStringWidthInPixels(item, style);

            var parts = item.Split(Separator);
            int partsCount = parts.Length;
            int maxPartWidth = 0;

            for (int i = 0; i < partsCount; i++)
            {
                int partWidth = GetStringWidthInPixels(parts[i], style) + i * indentWidth;

                if (partWidth > maxPartWidth)
                    maxPartWidth = partWidth;
            }

            return maxPartWidth;
        }

        private static int GetStringWidthInPixels(string item, GUIStyle style)
        {
            GUIContent itemContent = EditorDrawHelper.ContentCache.GetItem(item);
            Vector2 size = style.CalcSize(itemContent);
            return Convert.ToInt32(size.x);
        }
    }
}