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
        [SerializeField] public Sprite FireIcon;
        [SerializeField] public Sprite LockIcon;
        [SerializeField] public Sprite ElectricityIcon;
        [SerializeField] public Sprite RockIcon;

        private Dictionary<HazardType, Sprite> spriteMap;

        public Sprite GetSpriteByHazardType(HazardType hazardType)
        {
            if (spriteMap == null)
            {
                CreateSpriteMap();
            }
            Sprite returnSprite;
            if (spriteMap.TryGetValue(hazardType, out returnSprite))
            {
                return returnSprite;
            }

            return null;
        }

        private void CreateSpriteMap()
        {
            spriteMap = new Dictionary<HazardType, Sprite>()
            {
                {HazardType.Fire, FireIcon},
                {HazardType.Electricity, ElectricityIcon},
                {HazardType.Rock, RockIcon}
            };
        }
    }
}