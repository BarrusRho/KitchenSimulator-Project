using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenSimulator.ScriptableObjects
{
    [CreateAssetMenu()]
    public class AudioClipsSO : ScriptableObject
    {
        public AudioClip[] chopAudioClips;
        public AudioClip[] deliveryFailAudioClips;
        public AudioClip[] deliverySuccessAudioClips;
        public AudioClip[] footstepAudioClips;
        public AudioClip[] objectDropAudioClips;
        public AudioClip[] objectPickupAudioClips;
        public AudioClip[] trashAudioClips;
        public AudioClip[] warningAudioClips;
        public AudioClip stoveSizzleAudioClip;
    }
}
