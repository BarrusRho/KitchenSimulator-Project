using System;
using System.Collections;
using System.Collections.Generic;
using KitchenSimulator.CounterTops;
using UnityEngine;

namespace KitchenSimulator.UI
{
    public class StoveBurnFlashingBarUI : MonoBehaviour
    {
        private Animator _animator;
        
        [SerializeField] private StoveCounterTop _stoveCounterTop;

        private const string IS_FLASHING = "IsFlashing";

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            _stoveCounterTop.OnProgressChanged += OnProgressChanged;
            _animator.SetBool(IS_FLASHING, false);
        }

        private void OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs eventArgs)
        {
            var burnShowProgressAmount = 0.5f;
            var showIcon = _stoveCounterTop.IsFried() && eventArgs.progressNormalized >= burnShowProgressAmount;

            _animator.SetBool(IS_FLASHING, showIcon);
        }
    }
}