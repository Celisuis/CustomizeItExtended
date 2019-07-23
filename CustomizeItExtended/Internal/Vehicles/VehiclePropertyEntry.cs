using System;
using System.Collections.Generic;

namespace CustomizeItExtended.Internal.Vehicles
{
    [Serializable]
    public class VehiclePropertyEntry
    {
        public string Key;
        public CustomVehicleProperties Value;

        public VehiclePropertyEntry()
        {
        }

        public VehiclePropertyEntry(string key, CustomVehicleProperties value)
        {
            Key = key;
            Value = value;
        }

        public static implicit operator VehiclePropertyEntry(KeyValuePair<string, CustomVehicleProperties> kvp)
        {
            return new VehiclePropertyEntry(kvp.Key, kvp.Value);
        }

        public static implicit operator KeyValuePair<string, CustomVehicleProperties>(VehiclePropertyEntry entry)
        {
            return new KeyValuePair<string, CustomVehicleProperties>(entry.Key, entry.Value);
        }
    }
}