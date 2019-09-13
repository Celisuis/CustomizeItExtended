using System;
using System.Linq;
using CustomizeItExtended.Compatibility;
using CustomizeItExtended.Legacy;

// ReSharper disable InconsistentNaming

namespace CustomizeItExtended.Internal.Buildings
{
    [Serializable]
    public class BuildingProperties
    {
        public int m_academicBoostBonus;

        // Healthcare
        public int m_ambulanceCount;

        // Animals
        public int m_animalCount;

        // Attractiveness
        public int m_attractivenessAccumulation;
        public float m_batteryFactor;

        // Warehouses

        // Campus
        public float m_bonusEffectRadius;
        public int m_burialRate;
        public uint m_campusAttractiveness;

        // Disaster
        public int m_capacity;

        // Cargo
        public int m_cargoTransportAccumulation;
        public float m_cargoTransportRadius;
        public int m_cleaningRate;

        public float m_collectRadius;

        // General
        public int m_constructionCost;
        public int m_corpseCapacity;
        public int m_curingRate;
        public int m_deathCareAccumulation;
        public float m_deathCareRadius;
        public float m_detectionRange;
        public int m_disasterCoverageAccumulation;

        // Education
        public int m_educationAccumulation;
        public float m_educationRadius;

        // Electricity 
        public int m_electricityConsumption;
        public int m_electricityProduction;
        public int m_electricityStockpileAmount;
        public int m_electricityStockpileRate;

        // Entertainment
        public int m_entertainmentAccumulation;
        public float m_entertainmentRadius;
        public int m_evacuationBusCount;
        public float m_evacuationRange;
        public float m_extractRadius;
        public int m_extractRate;
        public int m_facultyBonusFactor;

        public int m_fireDepartmentAccumulation;
        public float m_fireDepartmentRadius;

        // Fire
        public int m_fireHazard;
        public int m_fireTolerance;
        public int m_fireTruckCount;
        public float m_firewatchRadius;

        // Garbage
        public int m_garbageAccumulation;
        public int m_garbageCapacity;
        public int m_garbageConsumption;
        public int m_garbageTruckCount;

        // Stockpile
        public int m_goodsConsumptionRate;
        public int m_goodsStockpileAmount;
        public int m_graveCount;
        public int m_healthBonus;
        public int m_healthCareAccumulation;
        public float m_healthCareRadius;

        // Death
        public int m_hearseCount;
        public int m_heatingProduction;

        // Vehicles
        public int m_helicopterCount;

        // Industries
        public int m_inputRate1;
        public int m_inputRate2;
        public int m_inputRate3;
        public int m_inputRate4;

        // Police
        public int m_jailCapacity;

        // Land Value
        public int m_landValueAccumulation;
        public int m_landValueBonus;
        public int m_mailCapacity;
        public int m_maintenanceCost;

        // Maintenance
        public float m_maintenanceRadius;
        public int m_maintenanceTruckCount;

        // Materials
        public int m_materialProduction;
        public int m_maxVehicleCount;
        public int m_maxVehicleCount2;
        public float m_maxWaterDistance;

        // Monuments
        public int m_monumentLevel;

        // Noise
        public int m_noiseAccumulation;
        public float m_noiseRadius;
        public int m_outletPollution;
        public int m_outputRate;
        public int m_outputVehicleCount;
        public int m_patientCapacity;
        public int m_policeCarCount;
        public int m_policeDepartmentAccumulation;
        public float m_policeDepartmentRadius;

        // Pollution
        public int m_pollutionAccumulation;
        public float m_pollutionRadius;
        public int m_postTruckCount;
        public int m_postVanCount;

        // Public Transport
        public int m_publicTransportAccumulation;
        public float m_publicTransportRadius;
        public int m_pumpingVehicles;
        public int m_residentCapacity;

        // Resources
        public int m_resourceCapacity;
        public int m_resourceConsumption;
        public int m_sentenceWeeks;
        public int m_serviceAccumulation;

        // Post Office
        public float m_serviceRadius;
        public int m_sewageAccumulation;
        public int m_sewageOutlet;
        public int m_sewageStorage;

        // Snow
        public int m_snowCapacity;
        public int m_snowConsumption;
        public int m_snowTruckCount;
        public int m_sortingRate;
        public int m_storageCapacity;
        public int m_studentCount;
        public int m_tourismBonus;
        public int m_touristFactor0;
        public int m_touristFactor1;
        public int m_touristFactor2;
        public int m_transmitterPower;
        public int m_truckCount;
        public bool m_useGroundWater;
        public int m_vehicleCount;
        public float m_vehicleRadius;

        // Visit Place Counts
        public int m_visitPlaceCount0;
        public int m_visitPlaceCount1;
        public int m_visitPlaceCount2;

        // Water, Sewage and Heating
        public int m_waterConsumption;
        public int m_waterIntake;
        public int m_waterOutlet;
        public int m_waterStockpileAmount;
        public int m_waterStockpileRate;
        public int m_waterStorage;

        // Workplace Counts
        public int m_workPlaceCount0;
        public int m_workPlaceCount1;
        public int m_workPlaceCount2;
        public int m_workPlaceCount3;
        
        // Nursing Home Mod
        public int numEducatedWorkers;
        public int numHighlyEducatedWorkers;
        public int numRooms;
        public float capacityModifier;
        public int numUneducatedWorkers;
        public int numWellEducatedWorkers;
        public float operationRadius;
        public int quality;

        public BuildingProperties()
        {
        }

        public BuildingProperties(BuildingInfo info)
        {
            var ai = info.m_buildingAI;

            var fields = ai.GetType().GetFields();

            var oldFields = fields.ToDictionary(field => field.Name);

            fields = GetType().GetFields();

            if (!CustomizeItExtendedMod.Settings.OverrideRebalancedIndustries)
                foreach (var customField in fields)
                {
                    if (RebalancedIndustries.IsRebalancedIndustriesActive() &&
                        RebalancedIndustries.RebalancedFields.Contains(customField.Name))
                        continue;

                    if (oldFields.ContainsKey(customField.Name))
                        customField.SetValue(this, oldFields[customField.Name].GetValue(ai));
                }
            else
                foreach (var customField in fields)
                    if (oldFields.ContainsKey(customField.Name))
                        customField.SetValue(this, oldFields[customField.Name].GetValue(ai));
        }

        public BuildingProperties(CustomizableProperties oldProps)
        {
            var fields = oldProps.GetType().GetFields();

            var originalFields = fields.ToDictionary(field => field.Name);

            fields = GetType().GetFields();

            if (!CustomizeItExtendedMod.Settings.OverrideRebalancedIndustries)
                foreach (var customField in fields)
                {
                    if (RebalancedIndustries.IsRebalancedIndustriesActive() &&
                        RebalancedIndustries.RebalancedFields.Contains(customField.Name))
                        continue;

                    if (originalFields.ContainsKey(customField.Name))
                        customField.SetValue(this, originalFields[customField.Name].GetValue(oldProps));
                }
            else
                foreach (var customField in fields)
                    if (originalFields.ContainsKey(customField.Name))
                        customField.SetValue(this, originalFields[customField.Name].GetValue(oldProps));
        }

        public static implicit operator BuildingProperties(CustomizableProperties props)
        {
            return new BuildingProperties(props);
        }
    }
}