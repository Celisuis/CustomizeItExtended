using System.Collections.Generic;

namespace CustomizeItExtended.Internal.Citizens
{
    public class CitizenEntry
    {
        public string Key;
        public CitizenProperties Value;

        public CitizenEntry()
        {
        }

        public CitizenEntry(string key, CitizenProperties value)
        {
            Key = key;
            Value = value;
        }

        public static implicit operator CitizenEntry(KeyValuePair<string, CitizenProperties> kvp)
        {
            return new CitizenEntry(kvp.Key, kvp.Value);
        }

        public static implicit operator KeyValuePair<string, CitizenProperties>(CitizenEntry entry)
        {
            return new KeyValuePair<string, CitizenProperties>(entry.Key, entry.Value);
        }
    }
}