using GameEvents;
using UnityEngine;

namespace Hazard
{
    public class ExecutionController : MonoBehaviour
    {
        private void Awake()
        {
            EventManager.AddListener(GameEvent.ExecutionTriggered, OnExecutionTriggered);
        }

        private void OnExecutionTriggered(IGameEvent eventparameters)
        {
            ExecutionTriggeredEventParams evtParams = (ExecutionTriggeredEventParams) eventparameters;
            if (HazardConfiguration.IsCombinationDeadly(evtParams.SelectedHazards))
            {
                Debug.Log("He's fucking dead.");
            }
            else
            {
                Debug.Log("He's still alive.");
            }
            
        }
    }
}