using CustomizeItExtended.GUI;
using CustomizeItExtended.GUI.Buildings;
using CustomizeItExtended.GUI.Citizens;
using CustomizeItExtended.GUI.Vehicles;
using ICities;
using UnityEngine;

namespace CustomizeItExtended.Extensions
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

                if (UIVehiclePanelWrapper.Instance != null && UIVehiclePanelWrapper.Instance.isVisible)
                {
                    UIVehiclePanelWrapper.Instance.isVisible = false;

                    UiUtils.DeepDestroy(UIVehiclePanelWrapper.Instance);
                }

                if (UICitizenPanelWrapper.Instance != null && UICitizenPanelWrapper.Instance.isVisible)
                {
                    UICitizenPanelWrapper.Instance.isVisible = false;

                    UiUtils.DeepDestroy(UICitizenPanelWrapper.Instance);
                }
            }
        }
    }
}