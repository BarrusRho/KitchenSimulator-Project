using System;
using KitchenSimulator.Audio;
using KitchenSimulator.Management;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KitchenSimulator.UI
{
    public class OptionsMenuUI : MonoBehaviour
    {
        [SerializeField] private Button _soundEffectsButton;
        [SerializeField] private Button _musicButton;
        [SerializeField] private Button _closeButton;
        [SerializeField] private TextMeshProUGUI _soundEffectsText;
        [SerializeField] private TextMeshProUGUI _musicText;

        private void Awake()
        {
            _soundEffectsButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.ChangeSoundEffectsVolume();
                UpdateOptionsMenuVisuals();
            });

            _musicButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.ChangebackgroundMusicVolume();
                UpdateOptionsMenuVisuals();
            });

            _closeButton.onClick.AddListener(() => { HideUI(); });
        }

        private void Start()
        {
            UpdateOptionsMenuVisuals();

            HideUI();

            GameManager.Instance.OnGameResumed += OnGameResumed;
        }

        private void UpdateOptionsMenuVisuals()
        {
            var soundEffectsVisual = Mathf.Round(AudioManager.Instance.GetSoundEffectsVolume() * 10f);
            _soundEffectsText.text = $"Sound Effects: {soundEffectsVisual}";

            var backgroundMusicVisual = Mathf.Round(AudioManager.Instance.GetBackgroundMusicVolume() * 10f);
            _musicText.text = $"Music: {backgroundMusicVisual}";
        }

        public void ShowUI()
        {
            this.gameObject.SetActive(true);
        }

        private void HideUI()
        {
            this.gameObject.SetActive(false);
        }

        private void OnGameResumed(object sender, EventArgs eventArgs)
        {
            HideUI();
        }
    }
}