namespace SolidUtilities.Editor
{
    using System;
    using System.Reflection;
    using JetBrains.Annotations;
    using UnityEditor;
    using UnityEditorInternal;
    using UnityEngine;
    using UnityEngine.Assertions;

#pragma warning disable 0649

    /// <summary>
    /// Scriptable object that holds references to resources needed for <see cref="EditorIcons"/>. With this database,
    /// we only need to know a GUID of the scriptable object instead of GUIDS or paths to all the resources used
    /// in <see cref="EditorIcons"/>.
    /// </summary>
    internal class EditorIconsDatabase : ScriptableObject
    {
        [UsedImplicitly]
        [SerializeField, Multiline(6)] private string _description;

        private const string TriangleRightData = "iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAABaklEQVQ4jdWRsWrCUBSGT87NrSGQ0WAgT9AH8AV8BXHqpJNDodClQ0GhSwuFLn2DQl/AxaXi0EWKUKhbcHBx65QrNAnnnnKjETW6t/90uNz/u+f/L/x/WUWCXq8HlmVBlmUghEDbtllrzcycnxv1+/1SYCwGc4mIwPf9MymlVkoxAAhEBAM5JXtLQhRKKWo0GvfVajUZj8ePs9nsW2sNlUpFMDMdY2w3YGZrEyHwff+m1Wp9drvdqzAM3TiOKU3TkvkwQp4XETPzWpZlYRAET51O56PZbF7UajWv5N4FrBlWMQghxA8RpVLK83q9/tJuty9L7t0O1inyskz7RESOlBJWq9X7ZDK5m06nbyX3ASDfgJkdUygifs3n84fBYPC6XC7ZcZySeQ/AzHrTQbRYLK5Ho9FzFEWpEAI8z0NtvuOI9gCu68JwOLxVSkGSJOC6LppzItJFPydL3LwOcRybLGhgxVanzH9AAPALQK6xKuQq1X8AAAAASUVORK5CYII=";
        private static Texture2D _triangleRight;
        public static Texture2D TriangleRight => LoadImage(ref _triangleRight, TriangleRightData);

        private const string ToolbarPlusSData = "iVBORw0KGgoAAAANSUhEUgAAACMAAAAgCAYAAACYTcH3AAAACXBIWXMAAAsTAAALEwEAmpwYAAAB2ElEQVRYhe2XwXHbMBBFHzC5x3EKMAuQRpsKzMzkbuWkY+gK7BJcQjqwrz5FvvugdLAaFRCpAYcVcHMgkFCUKMphaOfAN6MhQO5i/yyABeTMjP8F/9oCqgximhjENOHaDFar1aeiKO4BvPez8Xj8eMi+y+5802ZQFMW9mZ3GNvD+r6N1FROF1NuHcG474aqaAVPgBBBgDeSAAnMRWZhZu5guBBFfgbe1T5PwPA/iUjgiMx2F3D7Hp8/M3NT63ymzlId+AmS9i1HVBDirvNqISLrH9C7YAv3VmaTVIiAi69h2sY4cu1OacM49ee9no9HocblcngA/ayYPwHU1eBUzw/8LIWGw01gcRSQPwatcAD+0JNs3Rp/HQUa5aOtMgFtVXavqdEuM937mnHvqGjlOU+yLSB4W7SWw2eNyBnxT1evfY7QFUdWtw0ZEDvqY2U4FDuNMKavwlz1uHyeTyeLFTm0RmYtIBnxgN1MpvMIVQkSU3YKYQH9FL6Wckpuws+okdZfexASugCtVfYjBAinlAVll0beYyEX4NXEpImpm7Wumuu2fUQLyI2w2wGcRuYsvWjPjvZ9Vr53HKBERVdV3lFMilJeqhD/TNQ8LeQs3/KNsYBDTxCCmiV9+EbJgQNeTyQAAAABJRU5ErkJggg==";
        private static Texture2D _toolbarPlusS;
        public static Texture2D ToolbarPlusS => LoadImage(ref _toolbarPlusS, ToolbarPlusSData);
        
        private const string ToolbarPlusIData = "iVBORw0KGgoAAAANSUhEUgAAACAAAAAgCAYAAABzenr0AAAACXBIWXMAAAsTAAALEwEAmpwYAAABLUlEQVRYhe2XwU3EMBBF31gUsAp1RHEJi8QdbjnSwpZACZTAdY/ckYAOHG0DVLDaCjwciK0QLVlboDiH/JOdsTxfM/NnYlFVSsIU9b4SAK7mcNJ13QZ4Ah6AF2DXNM0nzBeB+945wB2wC4YYARGZvOFwONx67/cAxpi2ruvXqfMjdW1G5rhPjoD3fq+qlapWgUgGnoGPft0Bj8GQXAOqWp1bp8BaewK2o/uABaigOIFZZOic2wJvg083wDssIALFCcQUBJ2nVrhz7uwYFZGjMaYFJvtEQIxAjvMp5PaJ4imIBIwxrYgc/3rhIAVJiDXQ9/br3w6Oc26tnRweqX9ay0nBSmAlcAlDif6HXLMJhD6Rq/NLiH0gQbc/+kTmi8rxPYKHewBkfZqtBEoT+AJ2DGyZKhJ7cwAAAABJRU5ErkJggmCC";
        private static Texture2D _toolbarPlusI;
        public static Texture2D ToolbarPlusI => LoadImage(ref _toolbarPlusI, ToolbarPlusIData);

        [SerializeField] private Material _activeDarkSkin;
        [SerializeField] private Material _activeLightSkin; 
        [SerializeField] private Material _highlightedDarkSkin;
        [SerializeField] private Material _highlightedLightSkin;

        public Material Active => EditorGUIUtility.isProSkin ? _activeDarkSkin : _activeLightSkin;

        public Material Highlighted => EditorGUIUtility.isProSkin ? _highlightedDarkSkin : _highlightedLightSkin;
        
        private static Texture2D LoadImage(ref Texture2D texture, string base64Data)
        { 
            if (texture == null)
            {
                var pngBytes = Convert.FromBase64String(base64Data);
                texture = new Texture2D(2, 2);
                texture.LoadImage(pngBytes);
            }

            if (EditorGUIUtility.pixelsPerPoint > 1f)
            {
                SetPixelsPerPoint(texture, 2f);
            }

            if (!Mathf.Approximately(GetPixelsPerPoint(texture), EditorGUIUtility.pixelsPerPoint) && //scaling are different
                !Mathf.Approximately(EditorGUIUtility.pixelsPerPoint % 1, 0)) //screen scaling is non-integer
            {
                texture.filterMode = FilterMode.Bilinear;
            }
            
            return texture;
        }

        private static Func<Texture2D, float> _getPixelsPerPoint;

        private static float GetPixelsPerPoint(Texture2D texture)
        {
            if (_getPixelsPerPoint == null)
            {
                var pixelsPerPointMethod = typeof(Texture2D).GetMethod("get_pixelsPerPoint", BindingFlags.Instance | BindingFlags.NonPublic);
                Assert.IsNotNull(pixelsPerPointMethod);
                // ReSharper disable once AssignNullToNotNullAttribute
                _getPixelsPerPoint = (Func<Texture2D, float>) Delegate.CreateDelegate(typeof(Func<Texture2D, float>), pixelsPerPointMethod);
            }

            return _getPixelsPerPoint(texture);
        }
        
        private static Action<Texture2D, float> _setPixelsPerPoint;

        private static void SetPixelsPerPoint(Texture2D texture, float pixelsPerPoint)
        {
            if (_setPixelsPerPoint == null)
            {
                var pixelsPerPointMethod = typeof(Texture2D).GetMethod("set_pixelsPerPoint", BindingFlags.Instance | BindingFlags.NonPublic);
                Assert.IsNotNull(pixelsPerPointMethod);
                // ReSharper disable once AssignNullToNotNullAttribute
                _setPixelsPerPoint = (Action<Texture2D, float>) Delegate.CreateDelegate(typeof(Action<Texture2D, float>), pixelsPerPointMethod);
            }

            _setPixelsPerPoint(texture, pixelsPerPoint);
        }
    }
}