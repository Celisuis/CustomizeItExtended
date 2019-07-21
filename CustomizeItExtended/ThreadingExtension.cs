using CustomizeItExtended.GUI;
using ICities;
using UnityEngine;

namespace CustomizeItExtended
{
    public class ThreadingExtension : ThreadingExtensionBase
    {
        public override void OnUpdate(float realTimeDelta, float simulationTimeDelta)
        {
            base.OnUpdate(realTimeDelta, simulationTimeDelta);

            if (!LoadingExtension._isDoneLoading)
                return;

            if (Input.GetKeyUp(KeyCode.Escape))
            {
                if (UiPanelWrapper.Instance != null && UiPanelWrapper.Instance.isVisible)
                {
                    UiPanelWrapper.Instance.isVisible = false;
                    UiUtils.DeepDestroy(UiPanelWrapper.Instance);
                    return;
                }

                if (UIWarehousePanelWrapper.Instance != null && UIWarehousePanelWrapper.Instance.isVisible)
                {
                    UIWarehousePanelWrapper.Instance.isVisible = false;
                    UiUtils.DeepDestroy(UIWarehousePanelWrapper.Instance);
                }

                if (UIUniqueFactoryPanelWrapper.Instance != null && UIUniqueFactoryPanelWrapper.Instance.isVisible)
                {
                    UIUniqueFactoryPanelWrapper.Instance.isVisible = false;

                    UiUtils.DeepDestroy(UIUniqueFactoryPanelWrapper.Instance);
                }
            }
        }
    }
}