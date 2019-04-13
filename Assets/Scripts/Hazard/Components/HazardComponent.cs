using TMPro;
using UnityEngine;

namespace Hazard.Components
{
    public class HazardComponent : MonoBehaviour
    {
        [SerializeField] private TMP_Text _hazardName;

        private HazardType _hazardType;

        public HazardType HazardType
        {
            get { return _hazardType; }
        }

        public void SetHazardType(HazardType hazardType)
        {
            _hazardType = hazardType;

            _hazardName.text = hazardType.ToString();
        }
    }
}