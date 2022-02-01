namespace SolidUtilities.Editor
{
    using System;
    using JetBrains.Annotations;
    using UnityEditor;
    using UnityEngine;

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

        private const string ToolbarPlusSData = "iVBORw0KGgoAAAANSUhEUgAAACgAAAAgCAYAAABgrToAAAAACXBIWXMAAAsTAAALEwEAmpwYAAABaklEQVR42u2X7Q2DIBCGWYEVXIEVWMEVXMEVXMEVuoJ/Nf5xBUaoK1hMjubyFkSb9moaSd6kaQ94CveFWpZFnVnqArwA/xUwNvq+t16NV8fUetVeGu2fgOM45mSHYbivWj/n7BGQwJzXsqF5BU0C+s23dA8LEWjSFgH9nCoDhmqTgP7HlHCRqB0C+u8KOpkwzxFwwf5ASVfN16+lABuA0yoxyBeftlKAE57KBqCGPQoJQH69pcoMCqYgLQHIfatTB4cEYA3zbjxAjgBankreVciTkGamiO1E8GYX4CfgOCQAajq5rQS9RrD9CSAEwC0zv+OnKnLFiWgtKUdOiRM1IkGyZ1C1aWB9dxpAcAG+R3kqwFjl+Sog5EC3E7CTBDQwx+7wRW5fSVQSB9FpE3AGrneO1uJPN6wRxw9VpCEXqBOpphJr+alBnQ/k0+olUX/70UT+1WZAW6zN4q865m+87zPZZuF6uF+AP9QDxt5SDjxlseEAAAAASUVORK5CYII=";
        private static Texture2D _toolbarPlusS;
        public static Texture2D ToolbarPlusS => LoadImage(ref _toolbarPlusS, ToolbarPlusSData);
        
        private const string ToolbarPlusIData = "iVBORw0KGgoAAAANSUhEUgAAACAAAAAgCAQAAADZc7J/AAAACXBIWXMAAAsTAAALEwEAmpwYAAAGT2lUWHRYTUw6Y29tLmFkb2JlLnhtcAAAAAAAPD94cGFja2V0IGJlZ2luPSLvu78iIGlkPSJXNU0wTXBDZWhpSHpyZVN6TlRjemtjOWQiPz4gPHg6eG1wbWV0YSB4bWxuczp4PSJhZG9iZTpuczptZXRhLyIgeDp4bXB0az0iQWRvYmUgWE1QIENvcmUgNS42LWMxNDUgNzkuMTYzNDk5LCAyMDE4LzA4LzEzLTE2OjQwOjIyICAgICAgICAiPiA8cmRmOlJERiB4bWxuczpyZGY9Imh0dHA6Ly93d3cudzMub3JnLzE5OTkvMDIvMjItcmRmLXN5bnRheC1ucyMiPiA8cmRmOkRlc2NyaXB0aW9uIHJkZjphYm91dD0iIiB4bWxuczp4bXA9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC8iIHhtbG5zOmRjPSJodHRwOi8vcHVybC5vcmcvZGMvZWxlbWVudHMvMS4xLyIgeG1sbnM6cGhvdG9zaG9wPSJodHRwOi8vbnMuYWRvYmUuY29tL3Bob3Rvc2hvcC8xLjAvIiB4bWxuczp4bXBNTT0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wL21tLyIgeG1sbnM6c3RFdnQ9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9zVHlwZS9SZXNvdXJjZUV2ZW50IyIgeG1wOkNyZWF0b3JUb29sPSJBZG9iZSBQaG90b3Nob3AgQ0MgMjAxOSAoV2luZG93cykiIHhtcDpDcmVhdGVEYXRlPSIyMDIyLTAyLTAxVDE3OjU5OjA5KzAyOjAwIiB4bXA6TW9kaWZ5RGF0ZT0iMjAyMi0wMi0wMVQxODowNDowNiswMjowMCIgeG1wOk1ldGFkYXRhRGF0ZT0iMjAyMi0wMi0wMVQxODowNDowNiswMjowMCIgZGM6Zm9ybWF0PSJpbWFnZS9wbmciIHBob3Rvc2hvcDpDb2xvck1vZGU9IjEiIHhtcE1NOkluc3RhbmNlSUQ9InhtcC5paWQ6MDhhOWU0ODctYTA3Yy0zMzQ4LTgyMmItNTgxMDlhMTIzYTJkIiB4bXBNTTpEb2N1bWVudElEPSJhZG9iZTpkb2NpZDpwaG90b3Nob3A6ZGQ0YWZjOWQtYTExMi01NjQ2LWFkMjgtY2E1OGE3OTk1NDhhIiB4bXBNTTpPcmlnaW5hbERvY3VtZW50SUQ9InhtcC5kaWQ6YjM0NmM4ODQtZDc1ZC01MTQyLTg1ZjctOGRhNjRkMDBjYTY2Ij4gPHBob3Rvc2hvcDpUZXh0TGF5ZXJzPiA8cmRmOkJhZz4gPHJkZjpsaSBwaG90b3Nob3A6TGF5ZXJOYW1lPSJpIiBwaG90b3Nob3A6TGF5ZXJUZXh0PSJpIi8+IDwvcmRmOkJhZz4gPC9waG90b3Nob3A6VGV4dExheWVycz4gPHhtcE1NOkhpc3Rvcnk+IDxyZGY6U2VxPiA8cmRmOmxpIHN0RXZ0OmFjdGlvbj0iY3JlYXRlZCIgc3RFdnQ6aW5zdGFuY2VJRD0ieG1wLmlpZDpiMzQ2Yzg4NC1kNzVkLTUxNDItODVmNy04ZGE2NGQwMGNhNjYiIHN0RXZ0OndoZW49IjIwMjItMDItMDFUMTc6NTk6MDkrMDI6MDAiIHN0RXZ0OnNvZnR3YXJlQWdlbnQ9IkFkb2JlIFBob3Rvc2hvcCBDQyAyMDE5IChXaW5kb3dzKSIvPiA8cmRmOmxpIHN0RXZ0OmFjdGlvbj0ic2F2ZWQiIHN0RXZ0Omluc3RhbmNlSUQ9InhtcC5paWQ6MDhhOWU0ODctYTA3Yy0zMzQ4LTgyMmItNTgxMDlhMTIzYTJkIiBzdEV2dDp3aGVuPSIyMDIyLTAyLTAxVDE4OjA0OjA2KzAyOjAwIiBzdEV2dDpzb2Z0d2FyZUFnZW50PSJBZG9iZSBQaG90b3Nob3AgQ0MgMjAxOSAoV2luZG93cykiIHN0RXZ0OmNoYW5nZWQ9Ii8iLz4gPC9yZGY6U2VxPiA8L3htcE1NOkhpc3Rvcnk+IDwvcmRmOkRlc2NyaXB0aW9uPiA8L3JkZjpSREY+IDwveDp4bXBtZXRhPiA8P3hwYWNrZXQgZW5kPSJyIj8+tExcKgAAAMNJREFUSMdj/M9AGWAcEAOOKjGUA6lZNmf/k2nAGQZjIPWeQdn6PXkGwDS5Wu8hz4BVDKFA6qyNCdgLJ9DlXf6tZGBgCmfYgypsgQg4hiNpQGq1zXuoAf9QVb79LwRU9I5BGFmQCcUAGIAbgOKR/xjqgEyqGnC0AxwLZ20qyDVgN4MLkNpj4zpsDADGOyji8GaZd0zhFntwGHDsLSHtECOshGllAMVeGE0Hg8UA0goUDANILdLQDCCjUKXUgIGv2gaXAQASJ9MBHbJFvAAAAABJRU5ErkJggg==";
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

            return texture;
        }
    }
}