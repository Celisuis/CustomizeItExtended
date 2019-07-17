using CustomizeItEnhanced.Internal;
using ICities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace CustomizeItEnhanced
{
    public class LoadingExtension : LoadingExtensionBase
    {
        private bool isDoneLoading;

        private CustomizeItEnhancedTool Instance => CustomizeItEnhancedTool.instance;
        public override void OnLevelLoaded(LoadMode mode)
        {
            base.OnLevelLoaded(mode);

            if (mode == LoadMode.NewAsset || mode == LoadMode.LoadAsset || mode == LoadMode.NewMap || mode == LoadMode.LoadMap || mode == LoadMode.NewTheme || mode == LoadMode.LoadTheme)
                return;

            Instance.ToggleOptionsPanel(true);

            while (!isDoneLoading)
            {
                if (LoadingManager.instance.m_loadingComplete)
                {
                    CustomizeItEnhancedTool.instance.Initialize();
                    isDoneLoading = true;
                }
            }
        }
    }
}
