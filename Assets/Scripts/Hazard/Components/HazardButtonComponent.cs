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
            Locked
        }

        [SerializeField] private Button _hazardButton;
        [SerializeField] private Image _hazardIcon;

        private HazardButtonState _hazardButtonState;
        private HazardType _hazardType;

        private void Awake()
        {
            _hazardButton.onClick.AddListener(OnHazardButtonClicked);
            EventManager.AddListener(GameEvent.HazardAdded, OnHazardAdded);
            EventManager.AddListener(GameEvent.HazardRemoved, OnHazardRemoved);
            EventManager.AddListener(GameEvent.ExecutionTriggered, OnExecutionTriggered);
            EventManager.AddListener(GameEvent.ExecutionCompleted, OnExecutionCompleted);
        }

        private void OnDestroy()
        {
            _hazardButton.onClick.RemoveListener(OnHazardButtonClicked);
            EventManager.RemoveListener(GameEvent.HazardAdded, OnHazardAdded);
            EventManager.RemoveListener(GameEvent.HazardRemoved, OnHazardRemoved);
        }

        public void SetHazardType(HazardType hazardType)
        {
            _hazardType = hazardType;
            Sprite hazardSprite = Configurations.Configurations.SpriteConfiguration.GetSpriteByHazardType(hazardType);
            _hazardIcon.sprite = hazardSprite != null
                ? hazardSprite
                : Configurations.Configurations.SpriteConfiguration.LockIcon;

            if (hazardSprite == null)
            {
                SetButtonState(HazardButtonState.Locked);
            }
        }

        private void OnHazardButtonClicked()
        {
            if (_hazardButtonState != HazardButtonState.Locked)
            {
                if (_hazardButtonState == HazardButtonState.Select && gameObject.activeSelf)
                {
                    EventManager.CallEvent(GameEvent.AddHazard, new AddHazardEventParams(_hazardType));
                }
                else
                {
                    EventManager.CallEvent(GameEvent.RemoveHazard, new RemoveHazardEventParams(_hazardType));
                }
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

        private void OnExecutionTriggered(IGameEvent eventparameters)
        {
            gameObject.SetActive(false);
        }

        private void OnExecutionCompleted(IGameEvent eventparameters)
        {
            gameObject.SetActive(true);
        }
    }
}