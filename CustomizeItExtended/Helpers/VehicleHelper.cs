using System.Collections.Generic;
using System.Linq;
using CustomizeItExtended.Internal.Vehicles;

namespace CustomizeItExtended.Helpers
{
    public class VehicleHelper
    {
        public static List<VehicleInfo> GetAllVehicles()
        {
            List<VehicleInfo> vehicles = new List<VehicleInfo>();

            for (var x = 0; x < PrefabCollection<VehicleInfo>.LoadedCount(); x++)
            {
                var vehicle = PrefabCollection<VehicleInfo>.GetLoaded((uint) x);

                vehicles.Add(vehicle);
            }

            return vehicles;
        }

        public static List<string> GetAllVehicleNames()
        {
            List<string> vehicleNames = new List<string>();

            foreach (var vehicle in GetAllVehicles())
                if (!CustomizeItExtendedMod.Settings.AbsoluteNames)
                    vehicleNames.Add(
                        CustomizeItExtendedVehicleTool.instance.CustomVehicleNames.TryGetValue(vehicle.name,
                            out var nameProps)
                            ? nameProps.CustomName
                            : vehicle.name);
                else
                    vehicleNames.Add(vehicle.name);
            vehicleNames.Sort((x, y) => x.CompareTo(y));
            return vehicleNames;
        }

        public static string RetrieveOriginalVehicleName(string name)
        {
            foreach (var vehicleData in CustomizeItExtendedVehicleTool.instance.CustomVehicleNames)
                if (vehicleData.Value.CustomName == name || vehicleData.Key == name)
                    return vehicleData.Key;

            foreach (var vehicleData in CustomizeItExtendedVehicleTool.instance.OriginalVehicleNames)
                if (vehicleData.Key == name)
                    return vehicleData.Key;

            return name;
        }

        public static VehicleInfo RetrieveVehicleFromName(string name)
        {
            return GetAllVehicles().Find(x => x.name == name);
        }

        public static string RetrieveCurrentVehicleName(VehicleInfo vehicle)
        {
            return CustomizeItExtendedVehicleTool.instance.CustomVehicleNames.TryGetValue(vehicle.name, out var props)
                ? props.CustomName
                : vehicle.name;
        }

        public static VehicleInfo CreateCustomInfo(VehicleInfo source)
        {
            VehicleInfo info = new VehicleInfo();

            var sourceFields = source.GetType().GetFields();

            var newFields = info.GetType().GetFields().ToDictionary(field => field.Name);

            foreach (var field in sourceFields)
            {
                if(newFields.ContainsKey(field.Name))
                    newFields[field.Name].SetValue(info, field.GetValue(source));
            }

            return info;
        }

       
    }
}