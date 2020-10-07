namespace SolidUtilities.Editor.EditorWindows
{
    using Extensions;
    using UnityEditor;
    using UnityEngine;

    /// <summary>
    /// ProjectWindowUtil.CreateAsset() works well only when called inside OnGUI(). This is an editor window created
    /// solely for the purpose of invoking ProjectWindowUtil.CreateAsset() inside OnGUI().
    /// </summary>
    public class AssetCreator : EditorWindow
    {
        private Object _asset;
        private string _assetName;
        private bool _calledOnGuiOnce;

        /// <summary>
        /// Creates an instance of AssetCreator and invokes ProjectWindowUtil.CreateAsset() inside of its OnGUI().
        /// </summary>
        /// <param name="asset">The instance to save as an asset.</param>
        /// <param name="name">The default name of an asset.</param>
        public static void Create(Object asset, string name)
        {
            var window = CreateInstance<AssetCreator>();
            window.OnCreate(asset, name);
        }

        private void OnCreate(Object asset, string assetName)
        {
            _asset = asset;
            _assetName = assetName;
            this.Resize(1f, 1f);

            EditorApplication.projectChanged += Close;
            EditorApplication.quitting += Close;

            Show();
            Focus();
        }

        private void OnGUI()
        {
            if (_calledOnGuiOnce)
                return;

            _calledOnGuiOnce = true;
            ProjectWindowUtil.CreateAsset(_asset, _assetName);
            position = new Rect(Screen.currentResolution.width + 10f, Screen.currentResolution.height + 10f, 0f, 0f);
        }

        private void OnDestroy()
        {
            EditorApplication.projectChanged -= Close;
            EditorApplication.quitting -= Close;
        }
    }
}