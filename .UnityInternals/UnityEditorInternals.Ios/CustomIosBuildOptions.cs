namespace SolidUtilities.UnityEditorInternals
{
    using System.Collections.Generic;
    using UnityEditor.Modules;
    using UnityEditor.iOS;

    public static class CustomIosBuildOptions
    {
        private const string Ios = "iOS";

        private static CustomIosWindowExtension _customIosExtension;

        public static void AddIosOptionsDrawer(ICustomBuildOptionsDrawer customDrawer)
        {
            if (!ModuleManager.platformSupportModules.ContainsKey(Ios))
            {
                return;
            }

            if (_customIosExtension == null)
            {
                _customIosExtension = new CustomIosWindowExtension();
                ((UnityEditor.iOS.TargetExtension) ModuleManager.platformSupportModules[Ios]).buildWindow = _customIosExtension;
            }

            _customIosExtension.CustomDrawers.Add(customDrawer);
        }
    }

    internal class CustomIosWindowExtension : iOSBuildWindowExtension
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