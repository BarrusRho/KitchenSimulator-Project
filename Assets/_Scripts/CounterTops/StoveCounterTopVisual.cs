using System;
using UnityEngine;

namespace KitchenSimulator.CounterTops
{
    public class StoveCounterTopVisual : MonoBehaviour
    {
        [SerializeField] private StoveCounterTop _stoveCounterTop;
        [SerializeField] private GameObject _stoveOnGameObject;
        [SerializeField] private GameObject _stoveParticles;

        private void Start()
        {
            _stoveCounterTop.OnStateChanged += OnStateChanged;
        }

        private void OnStateChanged(object sender, StoveCounterTop.OnStateChangedEventArgs eventArgs)
        {
            var showVisual = eventArgs.fryingState == StoveCounterTop.FryingState.Frying ||
                              eventArgs.fryingState == StoveCounterTop.FryingState.Fried;
            
            _stoveOnGameObject.SetActive(showVisual);
            _stoveParticles.SetActive(showVisual);
        }
    }
}
