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
        [SerializeField] private Button _moveUpButton;
        [SerializeField] private Button _moveDownButton;
        [SerializeField] private Button _moveLeftButton;
        [SerializeField] private Button _moveRightButton;
        [SerializeField] private Button _interactButton;
        [SerializeField] private Button _interactAlternateButton;
        [SerializeField] private Button _pauseButton;
        [SerializeField] private Button _gamepadInteractButton;
        [SerializeField] private Button _gamepadInteractAlternateButton;
        [SerializeField] private Button _gamepadPauseButton;
        [SerializeField] private TextMeshProUGUI _soundEffectsText;
        [SerializeField] private TextMeshProUGUI _musicText;
        [SerializeField] private TextMeshProUGUI _moveUpButtonText;
        [SerializeField] private TextMeshProUGUI _moveDownButtonText;
        [SerializeField] private TextMeshProUGUI _moveLeftButtonText;
        [SerializeField] private TextMeshProUGUI _moveRightButtonText;
        [SerializeField] private TextMeshProUGUI _interactButtonText;
        [SerializeField] private TextMeshProUGUI _interactAlternateButtonText;
        [SerializeField] private TextMeshProUGUI _pauseButtonText;
        [SerializeField] private TextMeshProUGUI _gamepadInteractButtonText;
        [SerializeField] private TextMeshProUGUI _gamepadInteractAlternateButtonText;
        [SerializeField] private TextMeshProUGUI _gamepadPauseButtonText;
        [SerializeField] private Transform _rebindControlsPrompt;

        private Action _onCloseButtonAction;

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

            _closeButton.onClick.AddListener(() =>
            {
                HideUI();
                _onCloseButtonAction();
            });
            
            _moveUpButton.onClick.AddListener(() =>
            {
                RebindControls(InputManager.ControlBindings.MoveUp);
            });
            
            _moveDownButton.onClick.AddListener(() =>
            {
                RebindControls(InputManager.ControlBindings.MoveDown);
            });
            
            _moveLeftButton.onClick.AddListener(() =>
            {
                RebindControls(InputManager.ControlBindings.MoveLeft);
            });
            
            _moveRightButton.onClick.AddListener(() =>
            {
                RebindControls(InputManager.ControlBindings.MoveRight);
            });
            
            _interactButton.onClick.AddListener(() =>
            {
                RebindControls(InputManager.ControlBindings.Interact);
            });
            
            _interactAlternateButton.onClick.AddListener(() =>
            {
                RebindControls(InputManager.ControlBindings.InteractAlternate);
            });
            
            _pauseButton.onClick.AddListener(() =>
            {
                RebindControls(InputManager.ControlBindings.Pause);
            });
            
            _gamepadInteractButton.onClick.AddListener(() =>
            {
                RebindControls(InputManager.ControlBindings.GamepadInteract);
            });
            
            _gamepadInteractAlternateButton.onClick.AddListener(() =>
            {
                RebindControls(InputManager.ControlBindings.GamepadInteractAlternate);
            });
            
            _gamepadPauseButton.onClick.AddListener(() =>
            {
                RebindControls(InputManager.ControlBindings.GamepadPause);
            });
        }

        private void Start()
        {
            UpdateOptionsMenuVisuals();

            HideUI();
            HideRebindControlsPrompt();

            GameManager.Instance.OnGameResumed += OnGameResumed;
        }

        private void UpdateOptionsMenuVisuals()
        {
            var soundEffectsVisual = Mathf.Round(AudioManager.Instance.GetSoundEffectsVolume() * 10f);
            _soundEffectsText.text = $"Sound Effects: {soundEffectsVisual}";

            var backgroundMusicVisual = Mathf.Round(AudioManager.Instance.GetBackgroundMusicVolume() * 10f);
            _musicText.text = $"Music: {backgroundMusicVisual}";

            _moveUpButtonText.text = InputManager.Instance.GetControlBindingsText(InputManager.ControlBindings.MoveUp);
            _moveDownButtonText.text = InputManager.Instance.GetControlBindingsText(InputManager.ControlBindings.MoveDown);
            _moveLeftButtonText.text = InputManager.Instance.GetControlBindingsText(InputManager.ControlBindings.MoveLeft);
            _moveRightButtonText.text = InputManager.Instance.GetControlBindingsText(InputManager.ControlBindings.MoveRight);
            _interactButtonText.text = InputManager.Instance.GetControlBindingsText(InputManager.ControlBindings.Interact);
            _interactAlternateButtonText.text = InputManager.Instance.GetControlBindingsText(InputManager.ControlBindings.InteractAlternate);
            _pauseButtonText.text = InputManager.Instance.GetControlBindingsText(InputManager.ControlBindings.Pause);
            _gamepadInteractButtonText.text = InputManager.Instance.GetControlBindingsText(InputManager.ControlBindings.GamepadInteract);
            _gamepadInteractAlternateButtonText.text = InputManager.Instance.GetControlBindingsText(InputManager.ControlBindings.GamepadInteractAlternate);
            _gamepadPauseButtonText.text = InputManager.Instance.GetControlBindingsText(InputManager.ControlBindings.GamepadPause);
        }

        public void ShowUI(Action onCloseButtonAction)
        {
            this._onCloseButtonAction = onCloseButtonAction;
            this.gameObject.SetActive(true);
            _soundEffectsButton.Select();
        }

        private void HideUI()
        {
            this.gameObject.SetActive(false);
        }

        private void OnGameResumed(object sender, EventArgs eventArgs)
        {
            HideUI();
        }

        private void ShowRebindControlsPrompt()
        {
            _rebindControlsPrompt.gameObject.SetActive(true);
        }
        
        private void HideRebindControlsPrompt()
        {
            _rebindControlsPrompt.gameObject.SetActive(false);
        }

        private void RebindControls(InputManager.ControlBindings controlBindings)
        {
            ShowRebindControlsPrompt();
            InputManager.Instance.RebindControlBinding(controlBindings, ()=>
            {
                HideRebindControlsPrompt();
                UpdateOptionsMenuVisuals();
            });
        }
    }
}