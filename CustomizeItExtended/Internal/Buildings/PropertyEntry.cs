using System;
using System.Collections.Generic;
using CustomizeItExtended.Legacy;

namespace CustomizeItExtended.Internal.Buildings
{
    [Serializable]
    public class PropertyEntry
    {
        public string Key;
        public BuildingProperties Value;

        public PropertyEntry()
        {
        }

        public PropertyEntry(string key, BuildingProperties value)
        {
            Key = key;
            Value = value;
        }

        public static implicit operator PropertyEntry(KeyValuePair<string, BuildingProperties> kvp)
        {
            return new PropertyEntry(kvp.Key, kvp.Value);
        }

        public static implicit operator KeyValuePair<string, BuildingProperties>(PropertyEntry entry)
        {
            return new KeyValuePair<string, BuildingProperties>(entry.Key, entry.Value);
        }

        public static implicit operator PropertyEntry(CustomizablePropertiesEntry oldEntry)
        {
            return new PropertyEntry(oldEntry.Key, oldEntry.Value);
        }
    }
}