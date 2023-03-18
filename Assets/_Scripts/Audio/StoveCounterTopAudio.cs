using System;
using System.Collections;
using System.Collections.Generic;
using KitchenSimulator.CounterTops;
using UnityEngine;

namespace KitchenSimulator.Audio
{
    public class StoveCounterTopAudio : MonoBehaviour
    {
        private AudioSource _audioSource;
        [SerializeField] private StoveCounterTop _stoveCounterTop;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            _stoveCounterTop.OnStateChanged += OnStateChanged;
        }

        private void OnStateChanged(object sender, StoveCounterTop.OnStateChangedEventArgs eventArgs)
        {
            var canPlaySound = eventArgs.fryingState == StoveCounterTop.FryingState.Frying ||
                               eventArgs.fryingState == StoveCounterTop.FryingState.Fried;

            if (canPlaySound)
            {
                _audioSource.Play();
            }
            else
            {
                _audioSource.Pause();
            }
        }
    }
}
