using KitchenSimulator.Core;
using UnityEngine;

namespace KitchenSimulator.CounterTops
{
    public class SelectedCounterTopVisual : MonoBehaviour
    {
        [SerializeField] private CounterTopBase counterTopBase;
        [SerializeField] private GameObject[] _counterTopVisualArray;

        private void Start()
        {
            Player.Instance.OnSelectedCounterChanged += OnSelectedCounterChanged;
        }

        private void OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs eventArgs)
        {
            if (eventArgs.SelectedCounterTop == counterTopBase)
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
            foreach (var counterTopVisual in _counterTopVisualArray)
            {
                counterTopVisual.SetActive(true);
            }
        }

        private void Hide()
        {
            foreach (var counterTopVisual in _counterTopVisualArray)
            {
                counterTopVisual.SetActive(false);
            }
        }
    }
}