using System.Collections.Generic;
using Hazard;
using UnityEngine;

namespace Configurations
{
    public static class Configurations
    {
        public static SpriteConfiguration SpriteConfiguration;
    }

    [CreateAssetMenu(fileName = "SpriteConfiguration", menuName = "Configurations/SpriteConfiguration")]
    public class SpriteConfiguration : ScriptableObject
    {
        [Header("Hazard Icons")] [SerializeField]
        public Sprite FireIcon;

        [SerializeField] public Sprite LockIcon;
        [SerializeField] public Sprite ElectricityIcon;
        [SerializeField] public Sprite RockIcon;
        [SerializeField] public Sprite WaterIcn;

        [Header("Death Sprites")] 
        [SerializeField] public Sprite ElectrocutionSprite;
        [SerializeField] public Sprite RockDeathSprite;

        private Dictionary<HazardType, Sprite> hazardSpriteMap;
        private Dictionary<HazardType, Sprite> deathSpriteMap;

        public Sprite GetSpriteByHazardType(HazardType hazardType)
        {
            if (hazardSpriteMap == null)
            {
                CreateSpriteMap();
            }

            Sprite returnSprite;
            if (hazardSpriteMap.TryGetValue(hazardType, out returnSprite))
            {
                return returnSprite;
            }

            return null;
        }

        public Sprite GetDeathSprite(HazardType deathHazard)
        {
            if (deathSpriteMap == null)
            {
                CreateDeathSpriteMap();
            }
            
            Sprite returnSprite;
            if (deathSpriteMap.TryGetValue(deathHazard, out returnSprite))
            {
                return returnSprite;
            }

            return null;
        }

        private void CreateSpriteMap()
        {
            hazardSpriteMap = new Dictionary<HazardType, Sprite>()
            {
                {HazardType.Fire, FireIcon},
                {HazardType.Electricity, ElectricityIcon},
                {HazardType.Rock, RockIcon},
                {HazardType.Water, WaterIcn},
            };
        }

        private void CreateDeathSpriteMap()
        {
            deathSpriteMap = new Dictionary<HazardType, Sprite>()
            {
                {HazardType.Rock, RockDeathSprite},
                {HazardType.Electricity | HazardType.Water, ElectrocutionSprite}
            };
        }
    }
}