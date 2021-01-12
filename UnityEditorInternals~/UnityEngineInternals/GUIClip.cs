namespace SolidUtilities.UnityEngineInternals
{
    using UnityEngine;

    public static class GUIClip
    {
        public static Rect GetVisibleRect() => UnityEngine.GUIClip.visibleRect;
    }
}