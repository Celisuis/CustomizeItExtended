using ColossalFramework.IO;
using CustomizeItExtended.Internal;
using CustomizeItExtended.Internal.Buildings;
using CustomizeItExtended.Internal.Vehicles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace CustomizeItExtended.Settings
{
    [XmlRoot("CustomizeItExtended")]
    public class CustomizeItExtendedSettings
    {
        [XmlIgnore] private static readonly string ConfigPath =
            Path.Combine(DataLocation.localApplicationData, "CustomizeIt-Extended.xml");

        [XmlIgnore] private static readonly string DefaultConfigPath =
            Path.Combine(DataLocation.localApplicationData, "CustomizeIt-Extended-Default.xml");

        public float ButtonX;
        public float ButtonY;

        public List<CustomNameEntry> CustomNameEntries = new List<CustomNameEntry>();

        public List<CustomNameEntry> CustomVehicleNameEntries = new List<CustomNameEntry>();

        public List<PropertyEntry> Entries = new List<PropertyEntry>();

        public bool OverrideRebalancedIndustries;
        public float PanelX = 8f;
        public float PanelY = 65f;

        public bool RebalancedMessageShown;
        public bool SavePerCity;

        public List<VehiclePropertyEntry> VehicleEntries = new List<VehiclePropertyEntry>();


        public void Save()
        {
            if (!CustomizeItExtendedMod.Settings.SavePerCity)
            {
                Entries.Clear();

                foreach (var entry in CustomizeItExtendedTool.instance.CustomData)
                    if (entry.Value != null)
                        Entries.Add(entry);

                VehicleEntries.Clear();

                foreach (var vehicleEntry in CustomizeItExtendedVehicleTool.instance.CustomVehicleData)
                    if (vehicleEntry.Value != null)
                        VehicleEntries.Add(vehicleEntry);

                CustomNameEntries.Clear();

                foreach (var nameEntry in CustomizeItExtendedTool.instance.CustomBuildingNames)
                    if (nameEntry.Value != null)
                        CustomNameEntries.Add(nameEntry);

                CustomVehicleNameEntries.Clear();

                foreach (var vehicleName in CustomizeItExtendedVehicleTool.instance.CustomVehicleNames)
                    if (vehicleName.Value != null)
                        CustomVehicleNameEntries.Add(vehicleName);
            }

            var serializer = new XmlSerializer(typeof(CustomizeItExtendedSettings));

            using (var writer = new StreamWriter(ConfigPath))
            {
                serializer.Serialize(writer, CustomizeItExtendedMod.Settings);
            }
        }

        public void SaveDefaultConfig()
        {
            Entries.Clear();

            foreach (var entry in CustomizeItExtendedTool.instance.CustomData)
                if (entry.Value != null)
                    Entries.Add(entry);

            VehicleEntries.Clear();

            foreach (var vehicleEntry in CustomizeItExtendedVehicleTool.instance.CustomVehicleData)
                if (vehicleEntry.Value != null)
                    VehicleEntries.Add(vehicleEntry);

            CustomNameEntries.Clear();

            foreach (var nameEntry in CustomizeItExtendedTool.instance.CustomBuildingNames)
                if (nameEntry.Value != null)
                    CustomNameEntries.Add(nameEntry);

            CustomVehicleNameEntries.Clear();

            foreach (var vehicleName in CustomizeItExtendedVehicleTool.instance.CustomVehicleNames)
                if (vehicleName.Value != null)
                    CustomVehicleNameEntries.Add(vehicleName);

            var serializer = new XmlSerializer(typeof(CustomizeItExtendedSettings));

            using (var writer = new StreamWriter(DefaultConfigPath))
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

                    CustomizeItExtendedVehicleTool.instance.CustomVehicleData.Clear();

                    foreach (var vehicleEntry in config.VehicleEntries)
                        if (vehicleEntry != null)
                            CustomizeItExtendedVehicleTool.instance.CustomVehicleData.Add(vehicleEntry.Key,
                                vehicleEntry.Value);

                    foreach (var nameEntry in config.CustomNameEntries)
                        if (nameEntry != null)
                            CustomizeItExtendedTool.instance.CustomBuildingNames.Add(nameEntry.Key, nameEntry.Value);

                    foreach (var vehicleNameEntry in config.CustomVehicleNameEntries)
                        if (vehicleNameEntry != null)
                            CustomizeItExtendedVehicleTool.instance.CustomVehicleNames.Add(vehicleNameEntry.Key,
                                vehicleNameEntry.Value);

                    return config;
                }
            }
            catch (Exception e)
            {
                return new CustomizeItExtendedSettings();
            }
        }

        public static CustomizeItExtendedSettings LoadDefaultConfig()
        {
            var serializer = new XmlSerializer(typeof(CustomizeItExtendedSettings));

            try
            {
                using (var reader = new StreamReader(DefaultConfigPath))
                {
                    var config = (CustomizeItExtendedSettings) serializer.Deserialize(reader);


                    CustomizeItExtendedTool.instance.CustomData.Clear();

                    foreach (var entry in config.Entries)
                        if (entry != null)
                            CustomizeItExtendedTool.instance.CustomData.Add(entry.Key, entry.Value);

                    CustomizeItExtendedVehicleTool.instance.CustomVehicleData.Clear();

                    foreach (var vehicleEntry in config.VehicleEntries)
                        if (vehicleEntry != null)
                            CustomizeItExtendedVehicleTool.instance.CustomVehicleData.Add(vehicleEntry.Key,
                                vehicleEntry.Value);

                    foreach (var nameEntry in config.CustomNameEntries)
                        if (nameEntry != null)
                            CustomizeItExtendedTool.instance.CustomBuildingNames.Add(nameEntry.Key, nameEntry.Value);

                    foreach (var vehicleNameEntry in config.CustomVehicleNameEntries)
                        if (vehicleNameEntry != null)
                            CustomizeItExtendedVehicleTool.instance.CustomVehicleNames.Add(vehicleNameEntry.Key,
                                vehicleNameEntry.Value);

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