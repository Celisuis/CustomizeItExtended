using System.Collections.Generic;

namespace CustomizeItExtended.Translations.Languages
{
    public class German : BaseLanguage
    {
        public German()
        {
            FieldTranslations = new Dictionary<string, string>
            {
                {"Construction Cost", "Baukosten"},
                {"Maintenance Cost", "Unterhaltungskosten"},
                {"Electricity Consumption", "Stromverbrauch"},
                {"Water Consumption", "Wasserverbrauch"},
                {"Sewage Accumulation", "Wasserverschmutzung"},
                {"Garbage Accumulation", "Müllanhäufung"},
                {"Fire Hazard", "Brandgefahr"},
                {"Fire Tolerance", "Feuerresistenz"},
                {"Low Wealth Tourists", "Besucherplatz 0"},
                {"Medium Wealth Tourists", "Besucherplatz 1"},
                {"High Wealth Tourists", "Besucherplatz 2"},
                {"Entertainment Accumulation", "Unterhaltungs Zuwachs"},
                {"Entertainment Radius", "Unterhaltungs Radius"},
                {"Uneducated Workers", "Anzahl Arbeitsplätze 1"},
                {"Educated Workers", "Anzahl Arbeitsplätze 2"},
                {"Well Educated Workers", "Anzahl Arbeitsplätze 3"},
                {"Highly Educated Workers", "Anzahl Arbeitsplätze 4"},
                {"Noise Accumulation", "Lärm Zuwachs"},
                {"Noise Radius", "Lärm Radius"},
                {"Cargo Transport Accumulation", "Gütertransport Zuwachs"},
                {"Cargo Transport Radius", "Gütertransport Radius"},
                {"Hearse Count", "Anzahl Leichenwagen"},
                {"Corpse Capacity", "Anzahl Körper"},
                {"Burial Rate", "Beerdigung Geschw."},
                {"Grave Count", "Gräber Anzahl"},
                {"Deathcare Accumulation", "Bestattungswesen Zuwachs"},
                {"Deathcare Radius", "Bestattungswesen Radius"},
                {"Helicopter Count", "Anzahl Helikopter"},
                {"Vehicle Count", "Anzahl Fahrzeuge"},
                {"Detection Range", "Erkennungsweite"},
                {"Fire Department Accumulation", "Feuerwehr Zuwachs"},
                {"Fire Department Radius", "Feuerwehr Radius"},
                {"Fire Truck Count", "Anzahl Löschfahrzeuge"},
                {"Firewatch Radius", "Feuerwache Radius"},
                {"Education Accumulation", "Bildungs Zuwachs"},
                {"Education Radius", "Bildungs Radius"},
                {"Student Count", "Anzahl Schüler"},
                {"Resource Capacity", "Ressourcenkapazität"},
                {"Resource Consumption", "Ressourcenverbrauch"},
                {"Heating Production", "Heizleistung"},
                {"Pollution Accumulation", "Verschmutzung Zuwachs"},
                {"Pollution Radius", "Verschmutzung Radius"},
                {"Ambulance Count", "Anzahl Krankenwagen"},
                {"Patient Capacity", "Patientenkapazität"},
                {"Curing Rate", "Heilung Geschw"},
                {"Healthcare Accumulation", "Gesundheitswesen Zuwachs"},
                {"Healthcare Radius", "Gesundheitswesen Radius"},
                {"Animal Count", "Anzahl Tiere"},
                {"Garbage Collection Radius", "Sammel Radius"},
                {"Electricity Production", "Stromerzeugung"},
                {"Garbage Capacity", "Müll-Kapazität"},
                {"Garbage Consumption", "Müllverarbeitung"},
                {"Garbage Truck Count", "Anzahl Müllwagen"},
                {"Material Production", "Material Produktion"},
                {"Maintenance Radius", "Unterhaltungsradius"},
                {"Maintenance Truck Count", "Anz. Frzg. Strasseninst."},
                {"Monument Level", "Monument Level"},
                {"Attractiveness Accumulation", "Attraktivitäts Bonus"},
                {"Land Value Accumulation", "Bodenrichtwert Zuwachs"},
                {"Jail Capacity", "Gefängniss-Kapazität"},
                {"Police Car Count", "Anzahl Streifenwagen"},
                {"Police Department Radius", "Polizeistation Radius"},
                {"Police Department Accumulation", "Verschmutzung Zuwachs"},
                {"Sentence Weeks", "Wochen in Haft"},
                {"Battery Factor", "Batterie Faktor"},
                {"Transmitter Power", "Senderleistung"},
                {"Capacity", "Kapazität"},
                {"Disaster Coverage Accumulation", "Katastrophenschutz Zuwachs"},
                {"Electricity Stockpile Size", "Stromspeicherung (Menge)"},
                {"Electricty Stockpile Rate", "Stromspeicherung Geschw."},
                {"Water Stockpile Size", "Wasservorrat (Menge)"},
                {"Water Stockpile Rate", "Wasservorrat Geschw."},
                {"Goods Stockpile Size", "Gütervorrat (Menge)"},
                {"Goods Stockpile Rate", "Gütervorrat Geschw"},
                {"Snow Capacity", "Schnee-Kapazität"},
                {"Snow Consumption", "Schnee verarbeitung"},
                {"Snowplow Count", "Anzahl Schneepflüge"},
                {"Public Transport Accumulation", "ÖPNV Zuwachs"},
                {"Public Transport Radius", "ÖPNV Radius"},
                {"Resident Capacity", "Wohnkapazität"},
                {"Tourist Factor 1", "Tourismusfaktor 1"},
                {"Tourist Factor 2", "Tourismusfaktor 2"},
                {"Tourist Factor 3", "Tourismusfaktor 3"},
                {"Max Vehicle Count", "Max. Fahrzeuge"},
                {"Max Vehicle Count 2", "Max. Fahrzeuge 2"},
                {"Cleaning Rate", "Reinigung Geschw."},
                {"Max Water Distance", "Max. Wasserdistanz"},
                {"Outlet Pollution", "Ablauf Verschmutzung"},
                {"Pumping Vehicles", "Pumpfahrzeuge"},
                {"Sewage Outlet", "Abwasser abfluss (Menge)"},
                {"Sewage Storage", "Abwasserspeicher"},
                {"Use Ground Water", "Nutze Grundwasser"},
                {"Vehicle Radius", "Fahrzeug Radius"},
                {"Water Intake", "Wasser-aufnahme"},
                {"Water Outlet", "Wasser-abfluss"},
                {"Water Storage", "Wasserspeicher"},
                {"Service Radius", "Service Radius"},
                {"Service Accumulation", "Service Zuwachs"},
                {"Sorting Rate", "Sortier Geschw."},
                {"Mail Capacity", "Post-Kapazität"},
                {"Post Truck Count", "Anzahl Postlastwagen"},
                {"Post Van Count", "Anzahl Posttransporter"},
                {"Input Rate 1", "Inport Geschw. 1"},
                {"Input Rate 2", "Inport Geschw. 2"},
                {"Input Rate 3", "Inport Geschw. 3"},
                {"Input Rate 4", "Inport Geschw. 4"},
                {"Output Rate", "Export Geschw."},
                {"Output Vehicle Count", "Anzahl Export-Fahrzeuge"},
                {"Extract Radius", "Extrahierung Radius"},
                {"Extract Rate", "Extrahierun Geschw."},
                {"Storage Capacity", "Lager Kapazität"},
                {"Truck Count", "Anzahl Lastwagen"},
                {"Bonus Effect Radius", "Bonuseffekt Radius"},
                {"Land Value Bonus", "Bodenrichtwert Bonus"},
                {"Health Bonus", "Gesundheitsbonus"},
                {"Academic Boost Bonus", "Akademische förderung Bonus"},
                {"Tourism Bonus", "Tourismus Bonus"},
                {"Faculty Bonus Factor", "Fakultät Bonus Faktor"},
                {"Campus Attractiveness", "Campus Attraktivität"}
            };

            InformationTranslations = new Dictionary<string, string>
            {
                {
                    " A Customization and Information Viewer for Buildings, Vehicles and Citizens",
                    "Ein Anpassungs- und Informations-Fenster für Gebäude, Fahrzeuge und Bürger."
                },
                {"Save Per City", "Speichern pro Stadt"},
                {"This option is only available in the main menu.", "Diese Option ist nur im Hauptmenü verfügbar."},
                {"Override Rebalanced Industries", "Überschreibe \"Rebalanced Industries\""},
                {
                    "EXPERIMENTAL - This will cause your Industry buildings to revert back to Vanilla",
                    "EXPERIMENTELL - Dadurch werden deine Industriegebäude wieder in den Vanilla zustand gesetzt"
                },
                {"Reset ALL Buildings", "Alle Gebäude zurücksetzten"},
                {"The option is only available in game.", "Diese Option ist nur im Spiel verfügbar."},
                {"Import Old Settings", "Importiere alte Einstellungen"},
                {
                    "Note: This will import your old Customize It settings into Customize It Extended.",
                    "Achtung: Damit werden deine alten \"Customize It\" Einstellungen in \"Customize It Extended\" geladen."
                },
                {"No Old Settings found.", "Keine Einstellungen für Import gefunden"},
                {"City Configuration", "Stadt Einstellungen"},
                {"Import Default Config", "Importiere Standart Einstellungen"},
                {
                    "This will import your Default City Customize Config. WARNING - This will overwrite all current values.",
                    "Damit werden deine eigenen Einstellungen importiert. WARNUNG - überschreibt vorhandene Einstellungen."
                },
                {
                    "This option is only available in game and when Save Per City is enabled.",
                    "Diese Option ist nur im Spiel verfügbar und wenn \"Speichern pro Stadt\" aktiviert ist."
                },
                {"Export Current City to Default", "Exportiere Stadt als Standart"},
                {
                    "This will export your Current City Customized Options to the Default Profile",
                    "Damit wird deine jetzige Stadt als Standart-Profil gespeichert"
                },
                {"Set as Default Name?", "Standart Name"},
                {"DISABLED", "DEAKTIVIERT"}
            };

            CitizenTranslations = new Dictionary<string, string>
            {
                {"Age", "Alter"},
                {"Age Group", "Altersgruppe"},
                {"Crime Rate", "Kriminalitätsrate"},
                {"Criminal Status", "Kriminalitätsstatus"},
                {"Education Level", "Bildungsstufe"},
                {"Gender", "Geschlecht"},
                {"Happiness Level", "Glücklichkeit"},
                {"Health Level", "Gesundheitszustand"},
                {"Income Rate", "Einkommen"},
                {"Job Title", "Job Titel"},
                {"Wealth Status", "Vermögensstatus"},
                {"Wellbeing Level", "Wohlbefinden"},
                {"Work Efficiency", "Arbeitseffizienz"},
                {"Work Probability", "Arbeitswahrscheinlichkeit"}
            };

            Name = "German";
        }
    }
}