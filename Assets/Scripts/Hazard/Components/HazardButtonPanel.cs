using System;
using UnityEngine;

namespace Hazard.Components
{
    public class HazardButtonPanel : MonoBehaviour
    {
        [SerializeField] private HazardButtonComponent hazardButtonPrefab;

        private void Awake()
        {
            GenerateHazardButtons();
        }

        private void GenerateHazardButtons()
        {
            foreach (HazardType hazardType in Enum.GetValues(typeof(HazardType)))
            {
                if (hazardType != HazardType.None)
                {
                    HazardButtonComponent hazardButton = Instantiate(hazardButtonPrefab, gameObject.transform);
                    hazardButton.SetHazardType(hazardType);
                }
            }
        }
    }
}