namespace SolidUtilities.UnityEditorInternals
{
    using System.Collections.Generic;
    using UnityEditor.Android;

    public static class CustomAndroidBuildOptions
    {
        private static CustomAndroidWindowExtension _customAndroidExtension;

        public static void AddOptionsDrawer(ICustomBuildOptionsDrawer customDrawer)
        {
            if (_customAndroidExtension == null)
            {
                _customAndroidExtension = new CustomAndroidWindowExtension();
                UnityEditor.Android.TargetExtension.s_BuildWindow = _customAndroidExtension;
            }

            _customAndroidExtension.CustomDrawers.Add(customDrawer);
        }
    }

    internal class CustomAndroidWindowExtension : AndroidBuildWindowExtension
    {
        public readonly List<ICustomBuildOptionsDrawer> CustomDrawers = new List<ICustomBuildOptionsDrawer>();

        public override void ShowPlatformBuildOptions()
        {
            base.ShowPlatformBuildOptions();

            foreach (ICustomBuildOptionsDrawer customDrawer in CustomDrawers)
            {
                customDrawer.DrawBuildOptions();
            }
        }
    }
}