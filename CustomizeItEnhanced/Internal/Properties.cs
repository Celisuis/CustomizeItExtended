
using CustomizeItExtended.Legacy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CustomizeItExtended.Internal
{
    [Serializable]
    public class Properties
    {
        // General
        public int m_constructionCost;
        public int m_maintenanceCost;

        // Electricity 
        public int m_electricityConsumption;
        public int m_electricityProduction;
        public int m_electricityStockpileAmount;
        public int m_electricityStockpileRate;

        // Water, Sewage and Heating
        public int m_waterConsumption;
        public int m_sewageAccumulation;
        public int m_heatingProduction;
        public int m_cleaningRate;
        public float m_maxWaterDistance;
        public int m_outletPollution;
        public int m_pumpingVehicles;
        public int m_waterIntake;
        public int m_waterOutlet;
        public int m_waterStorage;
        public int m_sewageOutlet;
        public int m_sewageStorage;
        public bool m_useGroundWater;

        // Garbage
        public int m_garbageAccumulation;
        public int m_garbageCapacity;
        public int m_garbageConsumption;
        public int m_garbageTruckCount;
        public float m_collectRadius;

        // Fire
        public int m_fireHazard;
        public int m_fireTolerance;

        public int m_fireDepartmentAccumulation;
        public float m_fireDepartmentRadius;
        public int m_fireTruckCount;
        public float m_firewatchRadius;

        // Visit Place Counts
        public int m_visitPlaceCount0;
        public int m_visitPlaceCount1;
        public int m_visitPlaceCount2;

        // Entertainment
        public int m_entertainmentAccumulation;
        public float m_entertainmentRadius;

        // Workplace Counts
        public int m_workPlaceCount0;
        public int m_workPlaceCount1;
        public int m_workPlaceCount2;
        public int m_workPlaceCount3;

        // Noise
        public int m_noiseAccumulation;
        public float m_noiseRadius;

        // Cargo
        public int m_cargoTransportAccumulation;
        public float m_cargoTransportRadius;

        // Death
        public int m_hearseCount;
        public int m_corpseCapacity;
        public int m_burialRate;
        public int m_graveCount;
        public int m_deathCareAccumulation;
        public float m_deathCareRadius;

        // Vehicles
        public int m_helicopterCount;
        public int m_vehicleCount;
        public int m_maxVehicleCount;
        public int m_maxVehicleCount2;
        public float m_vehicleRadius;

        // Education
        public int m_educationAccumulation;
        public float m_educationRadius;
        public int m_studentCount;

        // Resources
        public int m_resourceCapacity;
        public int m_resourceConsumption;

        // Pollution
        public int m_pollutionAccumulation;
        public float m_pollutionRadius;

        // Healthcare
        public int m_ambulanceCount;
        public int m_patientCapacity;
        public int m_curingRate;
        public int m_healthCareAccumulation;
        public float m_healthCareRadius;

        // Animals
        public int m_animalCount;

        // Materials
        public int m_materialProduction;

        // Maintenance
        public float m_maintenanceRadius;
        public int m_maintenanceTruckCount;

        // Monuments
        public int m_monumentLevel;

        // Attractiveness
        public int m_attractivenessAccumulation;

        // Land Value
        public int m_landValueAccumulation;

        // Police
        public int m_jailCapacity;
        public int m_policeCarCount;
        public int m_policeDepartmentAccumulation;
        public float m_policeDepartmentRadius;
        public int m_sentenceWeeks;

        // Disaster
        public int m_capacity;
        public int m_disasterCoverageAccumulation;
        public int m_evacuationBusCount;
        public float m_evacuationRange;
        public float m_batteryFactor;
        public int m_transmitterPower;
        public float m_detectionRange;

        // Stockpile
        public int m_goodsConsumptionRate;
        public int m_goodsStockpileAmount;
        public int m_waterStockpileAmount;
        public int m_waterStockpileRate;

        // Snow
        public int m_snowCapacity;
        public int m_snowConsumption;
        public int m_snowTruckCount;

        // Public Transport
        public int m_publicTransportAccumulation;
        public float m_publicTransportRadius;
        public int m_residentCapacity;
        public int m_touristFactor0;
        public int m_touristFactor1;
        public int m_touristFactor2;

        // Post Office
        public float m_serviceRadius;
        public int m_serviceAccumulation;
        public int m_sortingRate;
        public int m_mailCapacity;
        public int m_postTruckCount;
        public int m_postVanCount;

        // Industries
        public int m_inputRate1;
        public int m_inputRate2;
        public int m_inputRate3;
        public int m_inputRate4;
        public int m_outputRate;
        public int m_outputVehicleCount;
        public float m_extractRadius;
        public int m_extractRate;
        public int m_storageCapacity;
        public int m_truckCount;

        // Campus
        public float m_bonusEffectRadius;
        public int m_landValueBonus;
        public int m_healthBonus;
        public int m_academicBoostBonus;
        public int m_tourismBonus;
        public int m_facultyBonusFactor;

        public Properties()
        {

        }

        public Properties(BuildingInfo info)
        {
            var ai = info.m_buildingAI;

            var fields = ai.GetType().GetFields();

            var oldFields = new Dictionary<string, FieldInfo>();

            foreach(var field in fields)
            {
                oldFields.Add(field.Name, field);
            }

            fields = GetType().GetFields();

            foreach(var customField in fields)
            {
                if(oldFields.ContainsKey(customField.Name))
                {
                    customField.SetValue(this, oldFields[customField.Name].GetValue(ai));
                }
            }
        }

        public Properties(CustomizableProperties oldProps)
        {
            var originalFields = new Dictionary<string, FieldInfo>();

            var fields = oldProps.GetType().GetFields();


            foreach(var field in fields)
            {
                originalFields.Add(field.Name, field);
            }

            fields = GetType().GetFields();

            foreach(var customField in fields)
            {
                if(originalFields.ContainsKey(customField.Name))
                {
                    customField.SetValue(this, originalFields[customField.Name].GetValue(oldProps));
                }
            }

        }

        public static implicit operator Properties(CustomizableProperties props)
        {
            return new Properties(props);
        }
    }
}
