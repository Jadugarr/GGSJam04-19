using DG.Tweening;
using GameEvents;
using SpriteGlow;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Serialization;

namespace Hazard
{
    public class ExecutionController : MonoBehaviour
    {
        [SerializeField] private Transform characterTransform;
        [SerializeField] private Transform originPosition;
        [SerializeField] private Transform executionPosition;

        [SerializeField] private float walkToExecutionDuration;
        [SerializeField] private float walkToResultDuration;

        [FormerlySerializedAs("walkToOrigingDuration")] [SerializeField]
        private float walkToOriginDuration;

        [SerializeField] private SpriteRenderer characterSpriteRenderer;

        [Header("Post Processing")] [SerializeField]
        private PostProcessVolume _postProcessVolume;

        [SerializeField] private SpriteGlowEffect _spriteGlowEffect;

        private Sprite defaultCharacterSprite;
        private HazardType _hazardsForExecution;
        private bool isDeadlyCombination;
        private Bloom _bloomSettings;

        private void Awake()
        {
            _postProcessVolume.profile.TryGetSettings(out _bloomSettings);
            defaultCharacterSprite = characterSpriteRenderer.sprite;
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
                Sprite deathSprite =
                    Configurations.Configurations.SpriteConfiguration.GetDeathSprite(_hazardsForExecution);

                if (deathSprite != null)
                {
                    characterSpriteRenderer.sprite = deathSprite;
                }

                tweenSequence.Append(characterTransform.DOMove(executionPosition.position, 2f));
                if (_bloomSettings)
                {
                    tweenSequence.Append(DOTween.To(() => _bloomSettings.intensity.value,
                        value => _bloomSettings.intensity.value = value, 23f, 3f));
                    tweenSequence.Join(DOTween.To(() => _spriteGlowEffect.OutlineWidth,
                        value => _spriteGlowEffect.OutlineWidth = value, 10, 3f));
                    
                    tweenSequence.onComplete += OnDeathAnimationComplete;
                }
            }
            else
            {
                Tween walkToOrigin =
                    characterTransform.DOMove(originPosition.position, walkToResultDuration);
                tweenSequence.Append(walkToOrigin);
                walkToOrigin.onComplete += OnWalkToOriginComplete;
            }

            
        }

        private void OnDeathAnimationComplete()
        {
            characterSpriteRenderer.sprite = defaultCharacterSprite;
            _bloomSettings.intensity.value = 0;
            _spriteGlowEffect.OutlineWidth = 0;
            
            Tween walkToOrigin =
                characterTransform.DOMove(originPosition.position, walkToResultDuration);
            walkToOrigin.onComplete += OnWalkToOriginComplete;
        }

        private void OnWalkToOriginComplete()
        {
            EventManager.CallEvent(GameEvent.ExecutionCompleted, new ExecutionCompletedEventParams());
        }
    }
}