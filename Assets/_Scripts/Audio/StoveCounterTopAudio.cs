using System;
using System.Collections;
using System.Collections.Generic;
using KitchenSimulator.CounterTops;
using KitchenSimulator.UI;
using UnityEngine;

namespace KitchenSimulator.Audio
{
    public class StoveCounterTopAudio : MonoBehaviour
    {
        private AudioSource _audioSource;
        [SerializeField] private StoveCounterTop _stoveCounterTop;
        private float _warningSoundTimer;
        private bool _canPlayWarningSound;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            _stoveCounterTop.OnStateChanged += OnStateChanged;
            _stoveCounterTop.OnProgressChanged += OnProgressChanged;
        }

        private void Update()
        {
            if (_canPlayWarningSound)
            {
                _warningSoundTimer -= Time.deltaTime;

                if (_warningSoundTimer <= 0f)
                {
                    var warningSoundTimerMaximum = 0.2f;
                    _warningSoundTimer = warningSoundTimerMaximum;
                    AudioManager.Instance.PlayBurnWarningSoundEffect(_stoveCounterTop.transform.position);
                }
            }
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

        private void OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs eventArgs)
        {
            var burnShowProgressAmount = 0.5f;
            _canPlayWarningSound = _stoveCounterTop.IsFried() && eventArgs.progressNormalized >= burnShowProgressAmount;
        }
    }
}