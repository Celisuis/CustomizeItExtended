// Credit to TPB - Original Source File added for Backwards Compatibility. See - https://github.com/TPBCS/CustomizeIt

using System;
using System.Collections.Generic;

namespace CustomizeItExtended.Legacy
{
    [Serializable]
    public class CustomizablePropertiesEntry
    {
        public string Key;
        public CustomizableProperties Value;

        public CustomizablePropertiesEntry()
        {
        }

        public CustomizablePropertiesEntry(string key, CustomizableProperties value)
        {
            Key = key;
            Value = value;
        }

        public static implicit operator CustomizablePropertiesEntry(
            KeyValuePair<string, CustomizableProperties> keyValuePair)
        {
            return new CustomizablePropertiesEntry(keyValuePair.Key, keyValuePair.Value);
        }

        public static implicit operator KeyValuePair<string, CustomizableProperties>(CustomizablePropertiesEntry entry)
        {
            return new KeyValuePair<string, CustomizableProperties>(entry.Key, entry.Value);
        }
    }
}