using System.Reflection;
using ColossalFramework.Globalization;
using ColossalFramework.Math;
using ColossalFramework.UI;

namespace CustomizeItExtended.Helpers
{
    public class CitizenHelper
    {
        /// <summary>
        /// Adapted from C.S Source Code
        /// </summary>
        /// <param name="citizenId"></param>
        /// <returns></returns>
        public static string GetDefaultJobTitle(uint citizenId)
        {
            string title = string.Empty;

            var workBuildingAI =
                BuildingManager.instance.m_buildings.m_buffer[
                        CitizenManager.instance.m_citizens.m_buffer[citizenId].m_workBuilding].Info
                    .m_buildingAI as CommonBuildingAI;

            var gender = CitizenManager.instance.m_citizens.m_buffer[citizenId].GetCitizenInfo(citizenId).m_gender;
            var education = CitizenManager.instance.m_citizens.m_buffer[citizenId].EducationLevel;

            if (workBuildingAI != null)
                title = workBuildingAI.GetTitle(gender, education,
                    CitizenManager.instance.m_citizens.m_buffer[citizenId].m_workBuilding, citizenId);

            if (title == string.Empty)
            {
                int number =
                    new Randomizer(CitizenManager.instance.m_citizens.m_buffer[citizenId].m_workBuilding + citizenId)
                        .Int32(1, 5);

                switch (education)
                {
                    case Citizen.Education.Uneducated:
                        title = Locale.Get(
                            gender != Citizen.Gender.Female
                                ? "CITIZEN_OCCUPATION_PROFESSION_UNEDUCATED"
                                : "CITIZEN_OCCUPATION_PROFESSION_UNEDUCATED_FEMALE", number.ToString());
                        break;
                    case Citizen.Education.OneSchool:
                        title = Locale.Get(
                            gender != Citizen.Gender.Female
                                ? "CITIZEN_OCCUPATION_PROFESSION_EDUCATED"
                                : "CITIZEN_OCCUPATION_PROFESSION_EDUCATED_FEMALE", number.ToString());
                        break;
                    case Citizen.Education.TwoSchools:
                        title = Locale.Get(
                            gender != Citizen.Gender.Female
                                ? "CITIZEN_OCCUPATION_PROFESSION_WELLEDUCATED"
                                : "CITIZEN_OCCUPATION_PROFESSION_WELLEDUCATED_FEMALE", number.ToString());
                        break;
                    case Citizen.Education.ThreeSchools:
                        title = Locale.Get(
                            gender != Citizen.Gender.Female
                                ? "CITIZEN_OCCUPATION_PROFESSION_HIGHLYEDUCATED"
                                : "CITIZEN_OCCUPATION_PROFESSION_HIGHLYEDUCATED_FEMALE", number.ToString());
                        break;
                }
            }


            return title + " " + Locale.Get("CITIZEN_OCCUPATION_LOCATIONPREPOSITION");
        }

        public static string GetEducationText(Citizen.Education education)
        {
            switch (education)
            {
                case Citizen.Education.OneSchool:
                    return "Elementary";
                case Citizen.Education.TwoSchools:
                    return "High School";
                case Citizen.Education.ThreeSchools:
                    return "University";
                case Citizen.Education.Uneducated:
                    return "Uneducated";
                default:
                    return "Error";
            }
        }

        public static string GetHealthText(Citizen.Health health)
        {
            switch (health)
            {
                case Citizen.Health.ExcellentHealth:
                    return "Excellent";
                case Citizen.Health.Healthy:
                    return "Healthy";
                case Citizen.Health.PoorHealth:
                    return "Poor";
                case Citizen.Health.Sick:
                    return "Sick";
                case Citizen.Health.VeryHealthy:
                    return "Very Health";
                case Citizen.Health.VerySick:
                    return "Very Sick";
                default:
                    return "Error";
            }
        }

        public static string GetWellbeingText(Citizen.Wellbeing wellbeing)
        {
            switch (wellbeing)
            {
                case Citizen.Wellbeing.Happy:
                    return "Happy";
                case Citizen.Wellbeing.Satisfied:
                    return "Satisfied";
                case Citizen.Wellbeing.Unhappy:
                    return "Unhappy";
                case Citizen.Wellbeing.VeryHappy:
                    return "Very Happy";
                case Citizen.Wellbeing.VeryUnhappy:
                    return "Very Unhappy";

                default:
                    return "Error";
            }
        }

        public static void UpdateJobTitle(HumanWorldInfoPanel infoPanel, string title)
        {
            UILabel jobLabel = infoPanel.GetType()
                .GetField("m_Occupation", BindingFlags.Instance | BindingFlags.NonPublic)
                ?.GetValue(infoPanel) as UILabel;

            //jobLabel.GetType().GetField("m_Text", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(jobLabel, title);
            jobLabel.GetType().GetField("text", BindingFlags.Instance | BindingFlags.Public).SetValue(jobLabel, title);
        }
    }
}