using DG.Tweening;
using GameEvents;
using UnityEngine;
using UnityEngine.Serialization;

namespace Hazard
{
    public class ExecutionController : MonoBehaviour
    {
        [SerializeField] private Transform characterTransform;
        [SerializeField] private Transform originPosition;
        [SerializeField] private Transform executionPosition;
        [SerializeField] private Transform resultPosition;

        [SerializeField] private float walkToExecutionDuration;
        [SerializeField] private float walkToResultDuration;
        [FormerlySerializedAs("walkToOrigingDuration")] [SerializeField] private float walkToOriginDuration;

        private HazardType _hazardsForExecution;
        private bool isDeadlyCombination;
        
        private void Awake()
        {
            characterTransform.position = originPosition.position;
            EventManager.AddListener(GameEvent.ExecutionTriggered, OnExecutionTriggered);
        }

        private void OnDestroy()
        {
            EventManager.RemoveListener(GameEvent.ExecutionTriggered, OnExecutionTriggered);
        }

        private void OnExecutionTriggered(IGameEvent eventparameters)
        {
            ExecutionTriggeredEventParams evtParams = (ExecutionTriggeredEventParams) eventparameters;
            _hazardsForExecution = evtParams.SelectedHazards;
            isDeadlyCombination = HazardConfiguration.IsCombinationDeadly(_hazardsForExecution);
            Tween walkToExecutionTween =
                characterTransform.DOMove(executionPosition.position, walkToExecutionDuration);
            walkToExecutionTween.onComplete += OnWalkToExecutionComplete;
        }

        private void OnWalkToExecutionComplete()
        {
            Sequence tweenSequence = DOTween.Sequence();
            if (isDeadlyCombination)
            {
                Debug.Log("He's fucking dead.");
                tweenSequence.Append(characterTransform.DORotate(new Vector3(0, 0, 180), 1f));
            }
            else
            {
                Debug.Log("He's still alive.");
            }
            
            // Play animation for death or whatever
            
            
            Tween walkToResultTween =
                characterTransform.DOMove(resultPosition.position, walkToResultDuration);
            tweenSequence.Append(walkToResultTween);
            walkToResultTween.onComplete += OnWalkToResultComplete;
        }

        private void OnWalkToResultComplete()
        {
            //I actually dunno if we wanna do something here
            
            Tween walkBackTween =
                characterTransform.DOMove(originPosition.position, walkToOriginDuration);
            walkBackTween.onComplete += OnWalkBackComplete;
        }

        private void OnWalkBackComplete()
        {
            EventManager.CallEvent(GameEvent.ExecutionCompleted, new ExecutionCompletedEventParams());
        }
    }
}