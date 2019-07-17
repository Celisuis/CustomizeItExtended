using CustomizeItExtended.Legacy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CustomizeItExtended.Internal
{
    [Serializable]
    public class PropertyEntry
    {
        public string Key;
        public Properties Value;

        public PropertyEntry()
        {

        }

        public PropertyEntry(string key, Properties value)
        {
            Key = key;
            Value = value;
        }

        public static implicit operator PropertyEntry(KeyValuePair<string, Properties> kvp)
        {
            return new PropertyEntry(kvp.Key, kvp.Value);
        }

        public static implicit operator KeyValuePair<string, Properties>(PropertyEntry entry)
        {
            return new KeyValuePair<string, Properties>(entry.Key, entry.Value);
        }

        public static implicit operator PropertyEntry(CustomizablePropertiesEntry oldEntry)
        {
            return new PropertyEntry(oldEntry.Key, oldEntry.Value);
        }
    }
}
