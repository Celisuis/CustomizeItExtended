using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CustomizeItExtended.Internal;
using CustomizeItExtended.Internal.Buildings;
using CustomizeItExtended.Internal.Vehicles;

namespace CustomizeItExtended.External
{
    public class CustomizeItExtendedAccess
    {
        public static Dictionary<string, NameProperties> CustomBuildingNames =>
            CustomizeItExtendedTool.instance.CustomBuildingNames;

        public static Dictionary<string, NameProperties> CustomVehicleNames =>
            CustomizeItExtendedVehicleTool.instance.CustomVehicleNames;
    }
}
