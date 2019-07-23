using ColossalFramework;
using CustomizeItExtended.Helpers;
using CustomizeItExtended.Internal.Buildings;
using CustomizeItExtended.Internal.Citizens;
using CustomizeItExtended.Internal.Vehicles;
using ICities;

namespace CustomizeItExtended.Extensions
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
                    CustomizeItExtendedCitizenTool.instance.Initialize();
                    CustomizeItExtendedVehicleTool.instance.Initialize();
                    InitializeCitizenJobData();
                    _isDoneLoading = true;
                }
        }

        private void InitializeCitizenJobData()
        {
            for (uint citizenID = 1; citizenID < CitizenManager.instance.m_citizens.m_buffer.Length; citizenID++)
            {
                var citizen = CitizenManager.instance.m_citizens.m_buffer[citizenID];

                if (!citizen.m_flags.IsFlagSet(Citizen.Flags.Created))
                    continue;

                if (CustomizeItExtendedCitizenTool.instance.OriginalJobTitles.TryGetValue(citizenID, out string _))
                    continue;

                CustomizeItExtendedCitizenTool.instance.OriginalJobTitles.Add(citizenID,
                    CitizenHelper.GetDefaultJobTitle(citizenID));
            }
        }
    }
}