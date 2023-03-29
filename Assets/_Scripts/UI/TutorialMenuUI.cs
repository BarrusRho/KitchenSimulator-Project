using System;
using KitchenSimulator.Management;
using TMPro;
using UnityEngine;

namespace KitchenSimulator.UI
{
    public class TutorialMenuUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _gamepadInteractKeyText;
        [SerializeField] private TextMeshProUGUI _gamepadAltKeyText;
        [SerializeField] private TextMeshProUGUI _gamepadPauseKeyText;
        
        [SerializeField] private TextMeshProUGUI _keyboardMoveKeyUpText;
        [SerializeField] private TextMeshProUGUI _keyboardMoveKeyLeftText;
        [SerializeField] private TextMeshProUGUI _keyboardMoveKeyDownText;
        [SerializeField] private TextMeshProUGUI _keyboardMoveKeyRightText;
        [SerializeField] private TextMeshProUGUI _keyboardInteractKeyText;
        [SerializeField] private TextMeshProUGUI _keyboardAltKeyUpText;
        [SerializeField] private TextMeshProUGUI _keyboardPauseKeyUpText;

        private void Start()
        {
            InputManager.Instance.OnBindingRebound += OnBindingRebound;
            GameManager.Instance.OnStateChanged += OnStateChanged;
            UpdateTutorialMenuVisuals();
            ShowUI();
        }

        private void OnBindingRebound(object sender, EventArgs eventArgs)
        {
            UpdateTutorialMenuVisuals();
        }
        
        private void OnStateChanged(object sender, EventArgs e)
        {
            if (GameManager.Instance.IsCountdownToStartActive())
            {
                HideUI();
            }
        }

        private void UpdateTutorialMenuVisuals()
        {
            _gamepadInteractKeyText.text = InputManager.Instance.GetControlBindingsText(InputManager.ControlBindings.GamepadInteract);
            _gamepadAltKeyText.text = InputManager.Instance.GetControlBindingsText(InputManager.ControlBindings.GamepadInteractAlternate);
            _gamepadPauseKeyText.text = InputManager.Instance.GetControlBindingsText(InputManager.ControlBindings.GamepadPause);
            
            _keyboardMoveKeyUpText.text = InputManager.Instance.GetControlBindingsText(InputManager.ControlBindings.MoveUp);
            _keyboardMoveKeyLeftText.text = InputManager.Instance.GetControlBindingsText(InputManager.ControlBindings.MoveLeft);
            _keyboardMoveKeyDownText.text = InputManager.Instance.GetControlBindingsText(InputManager.ControlBindings.MoveDown);
            _keyboardMoveKeyRightText.text = InputManager.Instance.GetControlBindingsText(InputManager.ControlBindings.MoveRight);
            _keyboardInteractKeyText.text = InputManager.Instance.GetControlBindingsText(InputManager.ControlBindings.Interact);
            _keyboardAltKeyUpText.text = InputManager.Instance.GetControlBindingsText(InputManager.ControlBindings.InteractAlternate);
            _keyboardPauseKeyUpText.text = InputManager.Instance.GetControlBindingsText(InputManager.ControlBindings.Pause);
        }

        private void ShowUI()
        {
            gameObject.SetActive(true);
        }

        private void HideUI()
        {
            gameObject.SetActive(false);
        }
    }
}
