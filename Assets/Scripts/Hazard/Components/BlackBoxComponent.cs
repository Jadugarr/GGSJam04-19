using GameEvents;
using UnityEngine;
using UnityEngine.UI;

namespace Hazard.Components
{
    public class BlackBoxComponent : MonoBehaviour
    {
        [SerializeField] private Transform[] selectedHazardPositions;
        [SerializeField] private Button executeButton;
        [SerializeField] private HazardComponent hazardPrefab;

        private HazardType selectedHazards;

        private HazardComponent[] availableSlots = new HazardComponent[3];

        private void Awake()
        {
            EventManager.AddListener(GameEvent.AddHazard, OnHazardAdded);
            EventManager.AddListener(GameEvent.RemoveHazard, OnRemoveHazard);

            executeButton.onClick.AddListener(OnExecuteClicked);
            CheckButtonState();
        }

        private void OnExecuteClicked()
        {
        }

        private void OnRemoveHazard(IGameEvent eventparameters)
        {
            RemoveHazardEventParams evtParams = (RemoveHazardEventParams) eventparameters;
            if (DestroyHazard(evtParams.HazardToRemove))
            {
                selectedHazards &= ~evtParams.HazardToRemove;

                CheckButtonState();
            }
        }

        private void OnHazardAdded(IGameEvent eventparameters)
        {
            AddHazardEventParams evtParams = (AddHazardEventParams) eventparameters;

            if (SpawnHazard(evtParams.HazardType))
            {
                selectedHazards |= evtParams.HazardType;

                CheckButtonState();
            }
        }

        private bool DestroyHazard(HazardType hazardType)
        {
            for (int i = 0; i < availableSlots.Length; i++)
            {
                HazardComponent hazardComponent = availableSlots[i];
                if (hazardComponent != null && hazardComponent.HazardType == hazardType)
                {
                    Destroy(hazardComponent.gameObject);
                    availableSlots[i] = null;
                    
                    EventManager.CallEvent(GameEvent.HazardRemoved, new HazardRemovedEventParams(hazardType));
                    
                    return true;
                }
            }

            return false;
        }

        private bool SpawnHazard(HazardType hazardType)
        {
            for (int i = 0; i < availableSlots.Length; i++)
            {
                if (availableSlots[i] == null)
                {
                    availableSlots[i] = Instantiate(hazardPrefab, selectedHazardPositions[i]);
                    availableSlots[i].SetHazardType(hazardType);
                    
                    EventManager.CallEvent(GameEvent.HazardAdded, new HazardAddedEventParams(hazardType));
                    
                    return true;
                }
            }

            return false;
        }

        private void CheckButtonState()
        {
            executeButton.gameObject.SetActive(selectedHazards != HazardType.None);
        }
    }
}