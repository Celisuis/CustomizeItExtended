using System.Collections.Generic;

namespace CustomizeItExtended.Internal
{
    public class CustomNameEntry
    {
        public string Key;
        public NameProperties Value;

        public CustomNameEntry()
        {
        }

        public CustomNameEntry(string key, NameProperties value)
        {
            Key = key;
            Value = value;
        }

        public static implicit operator CustomNameEntry(KeyValuePair<string, NameProperties> kvp)
        {
            return new CustomNameEntry(kvp.Key, kvp.Value);
        }

        public static implicit operator KeyValuePair<string, NameProperties>(CustomNameEntry entry)
        {
            return new KeyValuePair<string, NameProperties>(entry.Key, entry.Value);
        }
    }
}