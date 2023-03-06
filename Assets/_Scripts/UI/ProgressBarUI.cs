using KitchenSimulator.Core;
using KitchenSimulator.CounterTops;
using UnityEngine;
using UnityEngine.UI;

namespace KitchenSimulator.UI
{
    public class ProgressBarUI : MonoBehaviour
    {
        [SerializeField] private GameObject _hasProgressGameObject;
        [SerializeField] private Image _progressBarImage;
        private IHasProgress _objectHasProgress;

        private void Start()
        {
            _objectHasProgress = _hasProgressGameObject.GetComponent<IHasProgress>();

            if (_objectHasProgress == null)
            {
                Debug.LogError($"Game Object {_hasProgressGameObject} does not implement IHasProgress");
            }
            
            _objectHasProgress.OnProgressChanged += OnProgressChanged;
            _progressBarImage.fillAmount = 0f;
            HideProgressBar();
        }

        private void OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs eventArgs)
        {
            _progressBarImage.fillAmount = eventArgs.progressNormalized;

            if (eventArgs.progressNormalized == 0f || eventArgs.progressNormalized == 1f)
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
