using TMPro;
using UnityEngine;

namespace Hazard.Components
{
    public class HazardComponent : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _hazardIcon;

        private HazardType _hazardType;

        public HazardType HazardType
        {
            get { return _hazardType; }
        }

        public void SetHazardType(HazardType hazardType)
        {
            _hazardType = hazardType;

            _hazardIcon.sprite = Configurations.Configurations.SpriteConfiguration.GetSpriteByHazardType(hazardType);
        }
    }
}