using System;
using KitchenSimulator.Core;
using KitchenSimulator.CounterTops;
using KitchenSimulator.Management;
using KitchenSimulator.ScriptableObjects;
using UnityEngine;
using Random = UnityEngine.Random;

namespace KitchenSimulator.Audio
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioClipsSO _audioClipsSO;
        
        private void Start()
        {
            DeliveryManager.Instance.OnRecipeSuccess += OnRecipeSuccess;
            DeliveryManager.Instance.OnRecipeFailed += OnRecipeFailed;
            CuttingCounterTop.OnAnyCut += OnAnyCut;
            Player.Instance.OnPickedUpObject += OnPickedUpObject;
            CounterTopBase.OnAnyObjectDropped += OnAnyObjectDropped;
            TrashCounterTop.OnAnyObjectTrashed += OnAnyObjectTrashed;
            PlayerAudio.OnPlayerMovement += OnPlayerMovement;
        }
        
        private void PlaySoundEffect(AudioClip audioClip, Vector3 position, float volume = 1f)
        {
            AudioSource.PlayClipAtPoint(audioClip, position, volume);
        }

        private void PlaySoundEffectInArray(AudioClip[] audioClipArray, Vector3 position, float volume = 1f)
        {
            var randomAudioClip = Random.Range(0, audioClipArray.Length);
            PlaySoundEffect(audioClipArray[randomAudioClip], position, volume);
        }

        public void PlayFootstepsSoundEffect(Vector3 position, float volume)
        {
            PlaySoundEffectInArray(_audioClipsSO.footstepAudioClips, position, volume);
        }

        private void OnRecipeSuccess(object sender, EventArgs eventArgs)
        {
            var deliveryCounterTopPosition = DeliveryCounterTop.Instance.transform.position;
            PlaySoundEffectInArray(_audioClipsSO.deliverySuccessAudioClips, deliveryCounterTopPosition);
        }
        
        private void OnRecipeFailed(object sender, EventArgs eventArgs)
        {
            var deliveryCounterTopPosition = DeliveryCounterTop.Instance.transform.position;
            PlaySoundEffectInArray(_audioClipsSO.deliveryFailAudioClips, deliveryCounterTopPosition);
        }
        
        private void OnAnyCut(object sender, EventArgs eventArgs)
        {
            var cuttingCounterTop = sender as CuttingCounterTop;
            var cuttingCounterTopPosition = cuttingCounterTop.transform.position;
            PlaySoundEffectInArray(_audioClipsSO.chopAudioClips, cuttingCounterTopPosition);
        }
        
        private void OnPickedUpObject(object sender, EventArgs eventArgs)
        {
            var playerPosition = Player.Instance.transform.position;
            PlaySoundEffectInArray(_audioClipsSO.objectPickupAudioClips, playerPosition);
        }
        
        private void OnAnyObjectDropped(object sender, EventArgs eventArgs)
        {
            var baseCounterTop = sender as CounterTopBase;
            var baseCounterTopPosition = baseCounterTop.transform.position;
            PlaySoundEffectInArray(_audioClipsSO.objectDropAudioClips, baseCounterTopPosition);
        }
        
        private void OnAnyObjectTrashed(object sender, EventArgs eventArgs)
        {
            var trashCounterTop = sender as TrashCounterTop;
            var trashCounterTopPosition = trashCounterTop.transform.position;
            PlaySoundEffectInArray(_audioClipsSO.trashAudioClips, trashCounterTopPosition);
        }
        
        private void OnPlayerMovement(object sender, EventArgs eventArgs)
        {
            var player = sender as PlayerAudio;
            var playerPosition = player.transform.position;
            PlaySoundEffectInArray(_audioClipsSO.footstepAudioClips, playerPosition);
        }
    }
}
