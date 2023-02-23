using System;
using UnityEngine;

namespace KitchenSimulator.Core
{
    public class SelectedCounterVisual : MonoBehaviour
    {
        [SerializeField] private ClearCounter _clearCounter;
        [SerializeField] private GameObject _clearCounterVisual;
        private void Start()
        {
            Player.Instance.OnSelectedCounterChanged += OnSelectedCounterChanged;
        }

        private void OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs eventArgs)
        {
            if (eventArgs.selectedCounter == _clearCounter)
            {
                Show();
            }
            else
            {
                Hide();
            }
            
        }

        private void Show()
        {
            _clearCounterVisual.SetActive(true);
        }

        private void Hide()
        {
            _clearCounterVisual.SetActive(false);
        }
    }
}
