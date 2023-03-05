using KitchenSimulator.Core;
using KitchenSimulator.CounterTops;
using UnityEngine;
using UnityEngine.UI;

namespace KitchenSimulator.UI
{
    public class ProgressBarUI : MonoBehaviour
    {
        [SerializeField] private CuttingCounterTop _cuttingCounterTop;
        [SerializeField] private Image _progressBarImage;

        private void Start()
        {
            _cuttingCounterTop.OnCuttingProgressChanged += OnCuttingProgressChanged;
            _progressBarImage.fillAmount = 0f;
            HideProgressBar();
        }

        private void OnCuttingProgressChanged(object sender, CuttingCounterTop.OnCuttingProgressChangedEventArgs eventArgs)
        {
            _progressBarImage.fillAmount = eventArgs.cuttingProgressNormalized;

            if (eventArgs.cuttingProgressNormalized == 0f || eventArgs.cuttingProgressNormalized == 1f)
            {
                HideProgressBar();
            }
            else
            {
                ShowProgressBar();
            }
        }

        private void ShowProgressBar()
        {
            this.gameObject.SetActive(true);
        }

        private void HideProgressBar()
        {
            this.gameObject.SetActive(false);
        }
    }
}
