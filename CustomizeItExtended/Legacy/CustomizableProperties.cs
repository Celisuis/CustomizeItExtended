// Credit to TPB - Original Source File added for Backwards Compatibility. See - https://github.com/TPBCS/CustomizeIt

using System;
using System.Collections.Generic;
using System.Reflection;

// ReSharper disable InconsistentNaming

namespace CustomizeItExtended.Legacy
{
    [Serializable]
    public class CustomizableProperties
    {
        public int m_academicBoostBonus;
        public int m_ambulanceCount;
        public int m_animalCount;

        public int m_attractivenessAccumulation;
        public float m_batteryFactor;

        // C# Edit - Added Campus Support
        public float m_bonusEffectRadius;
        public int m_burialRate;
        public int m_capacity;
        public int m_cargoTransportAccumulation;
        public float m_cargoTransportRadius;
        public int m_cleaningRate;
        public float m_collectRadius;
        public int m_constructionCost;
        public int m_corpseCapacity;
        public int m_curingRate;
        public int m_deathCareAccumulation;
        public float m_deathCareRadius;
        public float m_detectionRange;
        public int m_disasterCoverageAccumulation;
        public int m_educationAccumulation;
        public float m_educationRadius;
        public int m_electricityConsumption;
        public int m_electricityProduction;
        public int m_electricityStockpileAmount;
        public int m_electricityStockpileRate;
        public int m_entertainmentAccumulation;
        public float m_entertainmentRadius;
        public int m_evacuationBusCount;
        public float m_evacuationRange;
        public float m_extractRadius;
        public int m_extractRate;
        public int m_facultyBonusFactor;
        public int m_fireDepartmentAccumulation;
        public float m_fireDepartmentRadius;
        public int m_fireHazard;
        public int m_fireTolerance;
        public int m_fireTruckCount;
        public float m_firewatchRadius;
        public int m_garbageAccumulation;
        public int m_garbageCapacity;
        public int m_garbageConsumption;
        public int m_garbageTruckCount;
        public int m_goodsConsumptionRate;
        public int m_goodsStockpileAmount;
        public int m_graveCount;
        public int m_healthBonus;
        public int m_healthCareAccumulation;
        public float m_healthCareRadius;
        public int m_hearseCount;
        public int m_heatingProduction;
        public int m_helicopterCount;

        // C# Edit - Added Industries Support
        public int m_inputRate1;
        public int m_inputRate2;
        public int m_inputRate3;
        public int m_inputRate4;
        public int m_jailCapacity;

        //public Rect m_animalArea;
        public int m_landValueAccumulation;
        public int m_landValueBonus;
        public int m_mailCapacity;
        public int m_maintenanceCost;
        public float m_maintenanceRadius;
        public int m_maintenanceTruckCount;
        public int m_materialProduction;
        public int m_maxVehicleCount;
        public int m_maxVehicleCount2;
        public float m_maxWaterDistance;
        public int m_monumentLevel;
        public int m_noiseAccumulation;
        public float m_noiseRadius;
        public int m_outletPollution;
        public int m_outputRate;
        public int m_outputVehicleCount;
        public int m_patientCapacity;
        public int m_policeCarCount;
        public int m_policeDepartmentAccumulation;
        public float m_policeDepartmentRadius;
        public int m_pollutionAccumulation;
        public float m_pollutionRadius;
        public int m_postTruckCount;
        public int m_postVanCount;
        public int m_publicTransportAccumulation;
        public float m_publicTransportRadius;
        public int m_pumpingVehicles;
        public int m_residentCapacity;
        public int m_resourceCapacity;
        public int m_resourceConsumption;
        public int m_sentenceWeeks;
        public int m_serviceAccumulation;

        // C# Edit - Added Post Office Values
        public float m_serviceRadius;
        public int m_sewageAccumulation;
        public int m_sewageOutlet;
        public int m_sewageStorage;
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
        public int m_visitPlaceCount0;
        public int m_visitPlaceCount1;
        public int m_visitPlaceCount2;
        public int m_waterConsumption;
        public int m_waterIntake;
        public int m_waterOutlet;
        public int m_waterStockpileAmount;
        public int m_waterStockpileRate;
        public int m_waterStorage;
        public int m_workPlaceCount0;
        public int m_workPlaceCount1;
        public int m_workPlaceCount2;
        public int m_workPlaceCount3;

        public CustomizableProperties()
        {
        }

        public CustomizableProperties(BuildingInfo building)
        {
            var ai = building.m_buildingAI;
            var fields = ai.GetType().GetFields();
            var buildingFields = new Dictionary<string, FieldInfo>();

            foreach (var field in fields)
                buildingFields.Add(field.Name, field);

            fields = GetType().GetFields();

            foreach (var field in fields)
                if (buildingFields.ContainsKey(field.Name))
                    field.SetValue(this, buildingFields[field.Name].GetValue(ai));
        }
    }
}