using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CustomizeItExtended.Internal.Buildings;

namespace CustomizeItExtended.Helpers
{
    public class BuildingHelper
    {
        public static List<BuildingInfo> GetAllBuildings()
        {
            List<BuildingInfo> buildings = new List<BuildingInfo>();

            for (var x = 0; x < PrefabCollection<BuildingInfo>.LoadedCount(); x++)
            {
                var building = PrefabCollection<BuildingInfo>.GetLoaded((uint) x);

                if (building.m_buildingAI == null)
                    continue;

                if (!building.m_buildingAI.GetType().IsSubclassOf(typeof(PlayerBuildingAI)))
                    continue;

                buildings.Add(building);
            }

            return buildings;
        }

        public static List<string> GetAllBuildingNames()
        {
            List<string> buildingNames = new List<string>();

            foreach (var building in GetAllBuildings())
            {
                buildingNames.Add(CustomizeItExtendedTool.instance.CustomBuildingNames.TryGetValue(building.name, out var nameProps) ? nameProps.CustomName : building.name);
            }
            buildingNames.Sort((x,y) => x.CompareTo(y));
            return buildingNames;
        }

        public static string RetrieveOriginalBuildingName(string name)
        {
            foreach (var buildingData in CustomizeItExtendedTool.instance.CustomBuildingNames)
            {
                if (buildingData.Value.CustomName == name || buildingData.Key == name)
                    return buildingData.Key;

            }

            foreach (var buildingData in CustomizeItExtendedTool.instance.OriginalBuildingNames)
            {
                if (buildingData.Key == name)
                    return buildingData.Key;
            }

            return string.Empty;
        }
    }
}
