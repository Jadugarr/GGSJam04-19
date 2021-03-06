using System.Collections.Generic;

namespace Hazard
{
    public static class HazardConfiguration
    {
        // Later the bool is gonna turn into the animation clip for the character's death
        private static Dictionary<HazardType, bool> hazardMap = new Dictionary<HazardType, bool>()
        {
            {HazardType.Water | HazardType.Electricity | HazardType.Salt, true},
            {HazardType.Fire | HazardType.Kerosine, true},
            {HazardType.Rock, true},
            {HazardType.Sandwich, true}
        };

        public static bool IsCombinationDeadly(HazardType encounteredHazards)
        {
            bool combinationValue;
            if (hazardMap.TryGetValue(encounteredHazards, out combinationValue))
            {
                return combinationValue;
            }

            return false;
        }
    }
}