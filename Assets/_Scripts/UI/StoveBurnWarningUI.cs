using System;
using System.Collections;
using System.Collections.Generic;
using KitchenSimulator.CounterTops;
using UnityEngine;

namespace KitchenSimulator.UI
{
    public class StoveBurnWarningUI : MonoBehaviour
    {
        [SerializeField] private StoveCounterTop _stoveCounterTop;

        private void Start()
        {
            _stoveCounterTop.OnProgressChanged += OnProgressChanged;
            
            HideUI();
        }

        private void OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs eventArgs)
        {
            var burnShowProgressAmount = 0.5f;
            var showIcon = _stoveCounterTop.IsFried() && eventArgs.progressNormalized >= burnShowProgressAmount;

            if (showIcon)
            {
                ShowUI();
            }
            else
            {
                HideUI();
            }
        }

        private void ShowUI()
        {
            this.gameObject.SetActive(true);
        }

        private void HideUI()
        {
            this.gameObject.SetActive(false);
        }
    }
}
