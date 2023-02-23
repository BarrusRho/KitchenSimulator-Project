using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace KitchenSimulator.Core
{
    public class SelectedCounterTopVisual : MonoBehaviour
    {
        [SerializeField] private ClearCounterTop clearCounterTop;
        [SerializeField] private GameObject _clearCounterTopVisual;
        private void Start()
        {
            Player.Instance.OnSelectedCounterChanged += OnSelectedCounterChanged;
        }

        private void OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs eventArgs)
        {
            if (eventArgs.SelectedCounterTop == clearCounterTop)
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
            _clearCounterTopVisual.SetActive(true);
        }

        private void Hide()
        {
            _clearCounterTopVisual.SetActive(false);
        }
    }
}
