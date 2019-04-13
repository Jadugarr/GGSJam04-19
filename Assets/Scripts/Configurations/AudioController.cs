using GameEvents;
using Hazard;
using UnityEngine;

namespace Configurations
{
    public class AudioController :MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        
        [SerializeField] private AudioClip _fieryDeathSound;
        [SerializeField] private AudioClip _crushedDeathSound;
        [SerializeField] private AudioClip _electroDeathSound;

        private void Awake()
        {
            EventManager.AddListener(GameEvent.Death, OnDeath);
            EventManager.AddListener(GameEvent.ExecutionCompleted, OnExecutionCompleted);
        }

        private void OnDestroy()
        {
            EventManager.RemoveListener(GameEvent.Death, OnDeath);
            EventManager.RemoveListener(GameEvent.ExecutionCompleted, OnExecutionCompleted);
        }

        private void OnDeath(IGameEvent eventparameters)
        {
            DeathEventParams evtParams = (DeathEventParams) eventparameters;

            if (evtParams.HazardType.HasFlag(HazardType.Fire))
            {
                _audioSource.clip = _fieryDeathSound;
                _audioSource.Play();
            }
            else if (evtParams.HazardType.HasFlag(HazardType.Rock))
            {
                _audioSource.clip = _crushedDeathSound;
                _audioSource.Play();
            }
            else if(evtParams.HazardType.HasFlag(HazardType.Electricity))
            {
                _audioSource.clip = _electroDeathSound;
                _audioSource.Play();
            }
        }

        private void OnExecutionCompleted(IGameEvent eventparameters)
        {
            _audioSource.Stop();
        }
    }
}