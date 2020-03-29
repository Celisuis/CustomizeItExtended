using System.Collections.Generic;
using ColossalFramework;
using ColossalFramework.UI;
using CustomizeItExtended.Extensions;
using PloppableRICO;

namespace CustomizeItExtended.Internal.Buildings.RICO
{
    public class CustomizeItExtendedRICOTool : Singleton<CustomizeItExtendedRICOTool>
    {
        internal Dictionary<string, RICOBuildingProperties> CustomRICOData = new Dictionary<string, RICOBuildingProperties>();
        internal Dictionary<string, RICOBuildingProperties> OriginalRICOData = new Dictionary<string, RICOBuildingProperties>();

        internal RICOBuilding CurrentSelectedRICOBuilding;

        internal BuildingInfo CurrentSelectedRICOBuildingInfo;

        private UIButton _residentialButton;

        private bool _buttonInitialized;

        private bool _isInitialized;

        internal InstanceID SelectedInstanceId;

        public void Initialize()
        {
            if (_isInitialized)
                return;
            
            AddPanelButton();

            _isInitialized = true;

        }

        private void AddPanelButton()
        {
            
        }
        
        public void SaveRICOBuilding(BuildingInfo info)
        {
            if(!CustomRICOData.TryGetValue(info.name, out RICOBuildingProperties props))
                CustomRICOData.Add(info.name, new RICOBuildingProperties(info, info.GetRICOBuildingData()));
            else
            {
                CustomRICOData[info.name] = new RICOBuildingProperties(info, info.GetRICOBuildingData()
                );
            }
            
            if(!CustomizeItExtendedMod.Settings.SavePerCity) CustomizeItExtendedMod.Settings.Save();
        }

        public void ResetRICOBuilding(BuildingInfo info)
        {
            var originalRICOProperties = info.GetOriginalRICOProperties();

            if (CustomRICOData.TryGetValue(info.name, out RICOBuildingProperties props))
                CustomRICOData.Remove(info.name);
            
            info.LoadRICOProperties(originalRICOProperties);
            
            
            if (!CustomizeItExtendedMod.Settings.SavePerCity) CustomizeItExtendedMod.Settings.Save();
        }
    }
}