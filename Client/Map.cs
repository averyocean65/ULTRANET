using System;

namespace ULTRANET
{
    public class MapConverter
    {
        private static string UKLayerToInt(string layer)
        {
            switch (layer)
            {
                case "Prelude":
                    return "0";
                case "Limbo":
                    return "1";
                case "Lust":
                    return "2";
                case "Gluttony":
                    return "3";
                case "Greed":
                    return "4";
                case "Wrath":
                    return "5";
                case "Heresy":
                    return "6";
                case "Violence":
                    return "7";
                case "Fraud":
                    return "8";
                case "Treachery":
                    return "9";
                case "PrimeSanctum":
                    return "P";

                default:
                    return "-1";
            }
        }

        public static string ToLevel(Map map)
        {
            if (map == Map.Endless || map == Map.uk_construct)
                return Enum.GetName(typeof(Map), map);

            // Get enum name
            string mapName = map.ToString();

            // Get number 
            string mapNumber = mapName.Substring(mapName.Length - 1);
            int levelNumber = int.Parse(mapNumber);

            // Get layer
            string mapLayer = mapName.Substring(0, mapName.Length - 1);
            string levelLayer = UKLayerToInt(mapLayer);

            return $"Level {levelLayer}-{levelNumber}";
        }
    }

    public enum Map
    {
        #region PRELUDE

        // Prelude
        Prelude1,
        Prelude2,
        Prelude3,
        Prelude4,
        Prelude5,
        PreludeS,

        #endregion

        #region ACT 1: INFINIE HYPERDEATH

        // Limbo
        Limbo1,
        Limbo2,
        Limbo3,
        Limbo4,
        LimboS,

        // Lust
        Lust1,
        Lust2,
        Lust3,
        Lust4,
        LustS,

        // Gluttony
        Gluttony1,
        Gluttony2,

        #endregion

        #region ACT 2: IMPERFECT HATRED

        // Greed
        Greed1,
        Greed2,
        Greed3,
        Greed4,
        GreedS,

        // Wrath
        Wrath1,
        Wrath2,
        Wrath3,
        Wrath4,
        WrathS,

        // Heresy
        Heresy1,
        Heresy2,

        #endregion

        #region ACT 3: GODFIST SUICIDE

        // TODO: Uncomment Violence, Fraud, and Treachery when ACT 3 is released
        // // Violence
        // Violence1, Violence2, Violence3, Violence4, ViolenceS,
        //
        // // Fraud
        // Fraud1, Fraud2, Fraud3, Fraud4, FraudS,
        //
        // // Treachery
        // Treachery1, Treachery2,

        #endregion

        #region PRIME SANCTUMS

        // TODO: Uncomment PrimeSanctum3 when P-3 is released
        PrimeSanctum1,
        PrimeSanctum2, /* PrimeSanctum3 */

        #endregion

        #region MISCELLANEOUS

        // Cybergrind
        Endless,

        // Sandbox
        uk_construct

        #endregion
    }
}