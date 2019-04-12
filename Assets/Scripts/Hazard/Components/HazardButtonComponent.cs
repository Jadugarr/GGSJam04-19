using GameEvents;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Hazard.Components
{
    public class HazardButtonComponent : MonoBehaviour
    {
        [SerializeField] private Button hazardButton;
        [SerializeField] private TMP_Text buttonText;

        private HazardType _hazardType;

        private void Awake()
        {
            hazardButton.onClick.AddListener(OnHazardButtonClicked);
        }

        public void SetHazardType(HazardType hazardType)
        {
            _hazardType = hazardType;
            buttonText.text = hazardType.ToString();
        }

        private void OnHazardButtonClicked()
        {
            EventManager.CallEvent(GameEvent.AddHazard, new AddHazardEventParams(_hazardType));
        }
    }
}