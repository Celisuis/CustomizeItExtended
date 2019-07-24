using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using CustomizeItExtended.Internal;
using CustomizeItExtended.Internal.Buildings;
using CustomizeItExtended.Internal.Citizens;
using CustomizeItExtended.Internal.Vehicles;
using ICities;

namespace CustomizeItExtended.Extensions
{
    public class SerializationExtension : SerializableDataExtensionBase
    {
        private static readonly string DataId = "Customize-It-Extended";
        private static readonly string BuildingNamesId = "Customize-It-Extended-Building-Names";
        private static readonly string VehicleNamesId = "Customize-It-Extended-Vehicle-Names";
        private static readonly string JobTitlesId = "Customize-It-Extended-Titles";
        private static readonly string VehicleDataId = "Customize-It-Vehicle-Data";
        private CustomizeItExtendedTool BuildingInstance => CustomizeItExtendedTool.instance;

        private CustomizeItExtendedVehicleTool VehicleInstance => CustomizeItExtendedVehicleTool.instance;

        private CustomizeItExtendedCitizenTool CitizenInstance => CustomizeItExtendedCitizenTool.instance;

        private List<PropertyEntry> CustomDataList
        {
            get
            {
                var list = new List<PropertyEntry>();

                if (BuildingInstance.CustomData == null)
                    return list;

                foreach (var item in BuildingInstance.CustomData)
                    list.Add(item);

                return list;
            }
            set
            {
                var collection = new Dictionary<string, Internal.Buildings.BuildingProperties>();

                if (value == null)
                    return;

                foreach (var item in value) collection.Add(item.Key, item.Value);

                BuildingInstance.CustomData = collection;
            }
        }

        public List<CustomNameEntry> CustomBuildingNames
        {
            get
            {
                var list = new List<CustomNameEntry>();

                if (BuildingInstance.CustomBuildingNames == null)
                    return list;

                foreach (var item in BuildingInstance.CustomBuildingNames)
                    list.Add(item);

                return list;
            }
            set
            {
                var collection = new Dictionary<string, NameProperties>();

                if (value == null)
                    return;

                foreach (var item in value)
                    collection.Add(item.Key, item.Value);

                BuildingInstance.CustomBuildingNames = collection;
            }
        }

        public List<CustomNameEntry> CustomVehicleNames
        {
            get
            {
                var list = new List<CustomNameEntry>();

                if (VehicleInstance.CustomVehicleNames == null)
                    return list;

                foreach (var item in VehicleInstance.CustomVehicleNames)
                    list.Add(item);

                return list;
            }
            set
            {
                var collection = new Dictionary<string, NameProperties>();

                if (value == null)
                    return;

                foreach (var item in value)
                    collection.Add(item.Key, item.Value);

                BuildingInstance.CustomBuildingNames = collection;
            }
        }

        public List<VehiclePropertyEntry> CustomVehicleProperties
        {
            get
            {
                var list = new List<VehiclePropertyEntry>();

                if (VehicleInstance.CustomVehicleData == null)
                    return list;

                foreach (var item in VehicleInstance.CustomVehicleData)
                    list.Add(item);

                return list;
            }
            set
            {
                var collection = new Dictionary<string, CustomVehicleProperties>();

                if (value == null)
                    return;

                foreach (var item in value) collection.Add(item.Key, item.Value);

                VehicleInstance.CustomVehicleData = collection;
            }
        }

        public Dictionary<uint, string> CustomJobTitles
        {
            get
            {
                var dictionary = new Dictionary<uint, string>();

                if (CitizenInstance.CustomJobTitles == null)
                    return dictionary;

                foreach (var kvp in CitizenInstance.CustomJobTitles)
                    dictionary.Add(kvp.Key, kvp.Value);

                return dictionary;
            }
            set
            {
                if (value == null)
                    return;

                CitizenInstance.CustomJobTitles = value;
            }
        }

        public override void OnSaveData()
        {
            base.OnSaveData();

            if (!CustomizeItExtendedMod.Settings.SavePerCity || BuildingInstance.CustomData == null)
                return;

            if (BuildingInstance.CustomData != null)
                using (var stream = new MemoryStream())
                {
                    var formatter = new BinaryFormatter();
                    formatter.Serialize(stream, CustomDataList);

                    serializableDataManager.SaveData(DataId, stream.ToArray());
                }

            if (BuildingInstance.CustomBuildingNames != null)
                using (var stream = new MemoryStream())
                {
                    var formatter = new BinaryFormatter();
                    formatter.Serialize(stream, CustomBuildingNames);

                    serializableDataManager.SaveData(BuildingNamesId, stream.ToArray());
                }

            if (CitizenInstance.CustomJobTitles != null)
                using (var stream = new MemoryStream())
                {
                    var formatter = new BinaryFormatter();
                    formatter.Serialize(stream, CustomJobTitles);

                    serializableDataManager.SaveData(JobTitlesId, stream.ToArray());
                }

            if (VehicleInstance.CustomVehicleData != null)
                using (var stream = new MemoryStream())
                {
                    var formatter = new BinaryFormatter();
                    formatter.Serialize(stream, CustomVehicleProperties);

                    serializableDataManager.SaveData(VehicleDataId, stream.ToArray());
                }

            if (VehicleInstance.CustomVehicleNames != null)
                using (var stream = new MemoryStream())
                {
                    var formatter = new BinaryFormatter();
                    formatter.Serialize(stream, CustomVehicleNames);

                    serializableDataManager.SaveData(VehicleNamesId, stream.ToArray());
                }
        }

        public override void OnLoadData()
        {
            base.OnLoadData();

            if (!CustomizeItExtendedMod.Settings.SavePerCity)
                return;

            var data = serializableDataManager.LoadData(DataId);


            if (data != null && data.Length > 0)
            {
                var formatter = new BinaryFormatter();

                using (var stream = new MemoryStream(data))
                {
                    CustomDataList = formatter.Deserialize(stream) as List<PropertyEntry>;
                }

                SimulationManager.instance.AddAction(() =>
                {
                    for (uint x = 0; x < PrefabCollection<BuildingInfo>.LoadedCount(); x++)
                        if (BuildingInstance.CustomData.TryGetValue(
                            PrefabCollection<BuildingInfo>.GetLoaded(x).name, out var customProps))
                            PrefabCollection<BuildingInfo>.GetLoaded(x).LoadProperties(customProps);
                });
            }

            data = serializableDataManager.LoadData(BuildingNamesId);

            if (data != null && data.Length > 0)
            {
                var formatter = new BinaryFormatter();

                using (var stream = new MemoryStream(data))
                {
                    CustomBuildingNames = formatter.Deserialize(stream) as List<CustomNameEntry>;
                }
            }

            data = serializableDataManager.LoadData(VehicleNamesId);

            if (data != null && data.Length > 0)
            {
                var formatter = new BinaryFormatter();

                using (var stream = new MemoryStream(data))
                {
                    CustomVehicleNames = formatter.Deserialize(stream) as List<CustomNameEntry>;
                }
            }

            data = serializableDataManager.LoadData(JobTitlesId);

            if (data != null && data.Length > 0)
            {
                var formatter = new BinaryFormatter();

                using (var stream = new MemoryStream(data))
                {
                    CustomJobTitles = formatter.Deserialize(stream) as Dictionary<uint, string>;
                }
            }

            data = serializableDataManager.LoadData(VehicleDataId);

            if (data != null && data.Length > 0)
            {
                var formatter = new BinaryFormatter();

                using (var stream = new MemoryStream(data))
                {
                    CustomVehicleProperties = formatter.Deserialize(stream) as List<VehiclePropertyEntry>;
                }

                SimulationManager.instance.AddAction(() =>
                {
                    for (uint x = 0; x < PrefabCollection<VehicleInfo>.LoadedCount(); x++)
                        if (VehicleInstance.CustomVehicleData.TryGetValue(
                            PrefabCollection<VehicleInfo>.GetLoaded(x).name, out var props))
                            PrefabCollection<VehicleInfo>.GetLoaded(x).LoadProperties(props);
                });
            }
        }
    }
}