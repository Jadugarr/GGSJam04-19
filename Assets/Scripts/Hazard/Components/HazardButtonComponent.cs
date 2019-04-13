using GameEvents;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Hazard.Components
{
    public class HazardButtonComponent : MonoBehaviour
    {
        private enum HazardButtonState
        {
            Select,
            Deselect,
        }
        
        [SerializeField] private Button _hazardButton;
        [SerializeField] private TMP_Text _buttonText;

        private HazardButtonState _hazardButtonState;

        private HazardType _hazardType;

        private void Awake()
        {
            _hazardButton.onClick.AddListener(OnHazardButtonClicked);
            EventManager.AddListener(GameEvent.HazardAdded, OnHazardAdded);
            EventManager.AddListener(GameEvent.HazardRemoved, OnHazardRemoved);
        }

        private void OnDestroy()
        {
            _hazardButton.onClick.RemoveListener(OnHazardButtonClicked);
        }

        public void SetHazardType(HazardType hazardType)
        {
            _hazardType = hazardType;
            _buttonText.text = hazardType.ToString();
        }

        private void OnHazardButtonClicked()
        {
            if (_hazardButtonState == HazardButtonState.Select)
            {
                EventManager.CallEvent(GameEvent.AddHazard, new AddHazardEventParams(_hazardType));
            }
            else
            {
                EventManager.CallEvent(GameEvent.RemoveHazard, new RemoveHazardEventParams(_hazardType));
            }
        }

        private void SetButtonState(HazardButtonState buttonState)
        {
            _hazardButtonState = buttonState;
            // change graphic on button
        }

        private void OnHazardAdded(IGameEvent eventparameters)
        {
            HazardAddedEventParams evtParams = (HazardAddedEventParams) eventparameters;

            if (evtParams.AddedHazard == _hazardType)
            {
                SetButtonState(HazardButtonState.Deselect);
            }
        }

        private void OnHazardRemoved(IGameEvent eventparameters)
        {
            HazardRemovedEventParams evtParams = (HazardRemovedEventParams) eventparameters;

            if (evtParams.RemovedHazard == _hazardType)
            {
                SetButtonState(HazardButtonState.Select);
            }
        }
    }
}