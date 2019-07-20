using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using ColossalFramework.IO;
using CustomizeItExtended.Internal;

namespace CustomizeItExtended.Settings
{
    [XmlRoot("CustomizeItExtended")]
    public class CustomizeItExtendedSettings
    {
        [XmlIgnore] private static readonly string ConfigPath =
            Path.Combine(DataLocation.localApplicationData, "CustomizeIt-Extended.xml");

        public float ButtonX;
        public float ButtonY;

        public List<PropertyEntry> Entries = new List<PropertyEntry>();
        public float PanelX = 8f;
        public float PanelY = 65f;
        public bool SavePerCity;

        public bool OverrideRebalancedIndustries;

        public bool RebalancedMessageShown;
        public void Save()
        {
            if (!CustomizeItExtendedMod.Settings.SavePerCity)
            {
                Entries.Clear();

                foreach (var entry in CustomizeItExtendedTool.instance.CustomData)
                {
                    if (entry.Value != null)
                    {
                        Entries.Add(entry);
                    }
                }
            }

            var serializer = new XmlSerializer(typeof(CustomizeItExtendedSettings));

            using (var writer = new StreamWriter(ConfigPath))
            {
                serializer.Serialize(writer, CustomizeItExtendedMod.Settings);
            }
        }

        public static CustomizeItExtendedSettings Load()
        {
            var serializer = new XmlSerializer(typeof(CustomizeItExtendedSettings));
            try
            {
                using (var reader = new StreamReader(ConfigPath))
                {
                    var config = (CustomizeItExtendedSettings) serializer.Deserialize(reader);

                    if (config.SavePerCity)
                        return config;

                    CustomizeItExtendedTool.instance.CustomData.Clear();

                    foreach (var entry in config.Entries)
                        if (entry != null)
                            CustomizeItExtendedTool.instance.CustomData.Add(entry.Key, entry.Value);

                    return config;
                }
            }
            catch (Exception e)
            {
                return new CustomizeItExtendedSettings();
            }
        }
    }
}