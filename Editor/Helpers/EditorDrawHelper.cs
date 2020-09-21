namespace SolidUtilities.Editor.Helpers
{
    using System;
    using UnityEditor;
    using UnityEngine;

    public static class EditorDrawHelper
    {
        public static readonly ContentCache ContentCache = new ContentCache();
        private const float PlaceholderIndent = 14f;

        private static readonly GUIStyle SearchToolbarStyle = new GUIStyle(EditorStyles.toolbar)
        {
            padding = new RectOffset(0, 0, 0, 0),
            stretchHeight = true,
            stretchWidth = true,
            fixedHeight = 0f
        };

        private static readonly GUIStyle InfoMessageStyle = new GUIStyle( "HelpBox")
        {
            margin = new RectOffset(4, 4, 2, 2),
            fontSize = 10,
            richText = true
        };

        private static readonly GUIStyle PlaceholderStyle = new GUIStyle(EditorStyles.centeredGreyMiniLabel)
        {
            alignment = TextAnchor.MiddleLeft,
            clipping = TextClipping.Clip,
            margin = new RectOffset(4, 4, 4, 4)
        };

        public static Vector2 DrawInScrollView(Vector2 scrollPos, Action drawContent)
        {
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
            drawContent();
            EditorGUILayout.EndScrollView();
            return scrollPos;
        }

        public static float DrawVertically(Action drawContent, GUILayoutOption option, Color backgroundColor)
        {
            Rect rect = EditorGUILayout.BeginVertical(option);
            EditorGUI.DrawRect(rect, backgroundColor);
            drawContent();
            EditorGUILayout.EndVertical();
            return rect.height;
        }

        public static Rect DrawVertically(Action drawContent)
        {
            Rect rect = EditorGUILayout.BeginVertical();
            drawContent();
            EditorGUILayout.EndVertical();
            return rect;
        }

        public static void DrawVertically(Action<Rect> drawContent)
        {
            Rect rect = EditorGUILayout.BeginVertical();
            drawContent(rect);
            EditorGUILayout.EndVertical();
        }

        public static void DrawBorders(float windowWidth, float windowHeight, Color color)
        {
            if (Event.current.type != EventType.Repaint)
                return;

            const float borderWidth = 1f;

            EditorGUI.DrawRect(new Rect(0f, 0f, borderWidth, windowHeight), color);
            EditorGUI.DrawRect(new Rect(0f, 0f, windowWidth, borderWidth), color);
            EditorGUI.DrawRect(new Rect(windowWidth - borderWidth, 0f, borderWidth, 0f), color);
            EditorGUI.DrawRect(new Rect(0f, windowHeight - borderWidth, windowWidth, borderWidth), color);
        }

        public static void DrawWithSearchToolbarStyle(Action drawToolbar, float toolbarHeight)
        {
            EditorGUILayout.BeginHorizontal(
                SearchToolbarStyle,
                GUILayout.Height(toolbarHeight),
                DrawHelper.ExpandWidth(false));

            drawToolbar();

            EditorGUILayout.EndHorizontal();
        }

        public static void DrawInfoMessage(string message)
        {
            var messageContent = new GUIContent(message, EditorIcons.Info);
            Rect labelPos = EditorGUI.IndentedRect(GUILayoutUtility.GetRect(messageContent, InfoMessageStyle));
            GUI.Label(labelPos, messageContent, InfoMessageStyle);
        }

        public static bool CheckIfChanged(Action drawContent)
        {
            EditorGUI.BeginChangeCheck();
            drawContent();
            return EditorGUI.EndChangeCheck();
        }

        public static string FocusedTextField(Rect rect, string text, string placeholder, GUIStyle style, string controlName)
        {
            GUI.SetNextControlName(controlName);
            text = EditorGUI.TextField(rect, text, style);
            EditorGUI.FocusTextInControl(controlName);

            if (Event.current.type == EventType.Repaint && string.IsNullOrEmpty(text))
            {
                var placeHolderArea = new Rect(rect.x + PlaceholderIndent, rect.y, rect.width - PlaceholderIndent, rect.height);
                GUI.Label(placeHolderArea, ContentCache.GetItem(placeholder), PlaceholderStyle);
            }

            return text;
        }
    }
}