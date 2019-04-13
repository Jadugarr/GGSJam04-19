using DG.Tweening;
using GameEvents;
using UnityEngine;
using UnityEngine.UI;

namespace Hazard.Components
{
    public class BlackBoxComponent : MonoBehaviour
    {
        [SerializeField] private Transform[] selectedHazardPositions;
        [SerializeField] private Transform combinedPosition;
        [SerializeField] private Button executeButton;
        [SerializeField] private HazardComponent hazardPrefab;
        [SerializeField] private float mergeDuration;
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
            if (HazardConfiguration.IsCombinationDeadly(selectedHazards))
            {
                Sequence tweenSequence = DOTween.Sequence();
                for (int i = 0; i < availableSlots.Length; i++)
                {
                    HazardComponent currentSlot = availableSlots[i];
                    if (currentSlot != null)
                    {
                        tweenSequence.Join(currentSlot.transform.DOMove(combinedPosition.position, mergeDuration));
                    }
                }

                tweenSequence.onComplete += OnCombinationComplete;
            }
            else
            {
                StartExecution();
            }
        }

        private void OnCombinationComplete()
        {
            StartExecution();
        }

        private void StartExecution()
        {
            EventManager.CallEvent(GameEvent.ExecutionTriggered, new ExecutionTriggeredEventParams(selectedHazards));
            ClearSelection();
        }

        private void ClearSelection()
        {
            for (int i = 0; i < availableSlots.Length; i++)
            {
                if (availableSlots[i] != null)
                {
                    DestroyHazard(availableSlots[i].HazardType);
                }
            }
        }

        private void OnRemoveHazard(IGameEvent eventparameters)
        {
            RemoveHazardEventParams evtParams = (RemoveHazardEventParams) eventparameters;
            DestroyHazard(evtParams.HazardToRemove);
        }

        private void OnHazardAdded(IGameEvent eventparameters)
        {
            AddHazardEventParams evtParams = (AddHazardEventParams) eventparameters;
            SpawnHazard(evtParams.HazardType);
        }

        private void DestroyHazard(HazardType hazardType)
        {
            for (int i = 0; i < availableSlots.Length; i++)
            {
                HazardComponent hazardComponent = availableSlots[i];
                if (hazardComponent != null && hazardComponent.HazardType == hazardType)
                {
                    Destroy(hazardComponent.gameObject);
                    availableSlots[i] = null;

                    EventManager.CallEvent(GameEvent.HazardRemoved, new HazardRemovedEventParams(hazardType));
                    selectedHazards &= ~hazardType;

                    CheckButtonState();
                    return;
                }
            }
        }

        private void SpawnHazard(HazardType hazardType)
        {
            for (int i = 0; i < availableSlots.Length; i++)
            {
                if (availableSlots[i] == null)
                {
                    availableSlots[i] = Instantiate(hazardPrefab, selectedHazardPositions[i]);
                    availableSlots[i].SetHazardType(hazardType);

                    EventManager.CallEvent(GameEvent.HazardAdded, new HazardAddedEventParams(hazardType));
                    selectedHazards |= hazardType;

                    CheckButtonState();

                    return;
                }
            }
        }

        private void CheckButtonState()
        {
            executeButton.gameObject.SetActive(selectedHazards != HazardType.None);
        }
    }
}