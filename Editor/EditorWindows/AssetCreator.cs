namespace SolidUtilities.Editor.EditorWindows
{
    using Extensions;
    using JetBrains.Annotations;
    using UnityEditor;
    using Object = UnityEngine.Object;

    /// <summary>
    /// ProjectWindowUtil.CreateAsset() works well only when called inside OnGUI(). This is an editor window created
    /// solely for the purpose of invoking ProjectWindowUtil.CreateAsset() inside OnGUI().
    /// </summary>
    [PublicAPI] public class AssetCreator : EditorWindow
    {
        private Object _asset;
        private string _assetName;
        private bool _calledOnGuiOnce;
        private bool _receivedOnProjectChangeOnce;

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

            EditorApplication.quitting += Close;

            Show();
            Focus();
        }

        private void OnProjectChange()
        {
            // The first time OnProjectChange is called is when the asset is created and interactive renaming is initiated.
            // The second time - is when the asset was renamed.
            if ( ! _receivedOnProjectChangeOnce)
            {
                _receivedOnProjectChangeOnce = true;
                return;
            }

            Close();
        }

        private void OnGUI()
        {
            if (_calledOnGuiOnce)
                return;

            _calledOnGuiOnce = true;
            ProjectWindowUtil.CreateAsset(_asset, _assetName);
            this.MoveOutOfScreen();
        }

        private void OnDestroy()
        {
            EditorApplication.quitting -= Close;
        }
    }
}