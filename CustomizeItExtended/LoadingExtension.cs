using CustomizeItExtended.Internal;
using ICities;

namespace CustomizeItExtended
{
    public class LoadingExtension : LoadingExtensionBase
    {
        public static bool _isDoneLoading;

        private CustomizeItExtendedTool Instance => CustomizeItExtendedTool.instance;

        public override void OnLevelLoaded(LoadMode mode)
        {
            base.OnLevelLoaded(mode);

            if (mode == LoadMode.NewAsset || mode == LoadMode.LoadAsset || mode == LoadMode.NewMap ||
                mode == LoadMode.LoadMap || mode == LoadMode.NewTheme || mode == LoadMode.LoadTheme)
                return;

            Instance.ToggleOptionsPanel(true);

            while (!_isDoneLoading)
                if (LoadingManager.instance.m_loadingComplete)
                {
                    CustomizeItExtendedTool.instance.Initialize();
                    _isDoneLoading = true;
                }
        }
    }
}