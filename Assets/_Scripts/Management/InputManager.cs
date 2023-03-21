using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace KitchenSimulator.Management
{
    public class InputManager : MonoBehaviour
    {
        public static InputManager Instance { get; private set; }

        private PlayerInputActions _playerInputActions;

        public enum ControlBindings
        {
            MoveUp,
            MoveDown,
            MoveLeft,
            MoveRight,
            Interact,
            InteractAlternate,
            Pause
        }

        public event EventHandler OnInteractAction;
        public event EventHandler OnInteractAlternateAction;
        public event EventHandler OnPauseAction;

        private const string PLAYER_PREFS_BINDINGS = "InputBindings";

        private void Awake()
        {
            Instance = this;

            InitializePlayerInputActions();
        }

        private void InitializePlayerInputActions()
        {
            _playerInputActions = new PlayerInputActions();
            
            if (PlayerPrefs.HasKey(PLAYER_PREFS_BINDINGS))
            {
                var savedInputs = PlayerPrefs.GetString(PLAYER_PREFS_BINDINGS);
                _playerInputActions.LoadBindingOverridesFromJson(savedInputs);
            }
            
            _playerInputActions.Player.Enable();
            _playerInputActions.Player.Interact.performed += OnInteractPerformed;
            _playerInputActions.Player.InteractAlternate.performed += OnInteractAlternatePerformed;
            _playerInputActions.Player.Pause.performed += OnPause;
        }

        private void OnDestroy()
        {
            _playerInputActions.Player.Interact.performed -= OnInteractPerformed;
            _playerInputActions.Player.InteractAlternate.performed -= OnInteractAlternatePerformed;
            _playerInputActions.Player.Pause.performed -= OnPause;

            _playerInputActions.Dispose();
        }

        private void OnInteractPerformed(InputAction.CallbackContext callbackContext)
        {
            OnInteractAction?.Invoke(this, EventArgs.Empty);
        }

        private void OnInteractAlternatePerformed(InputAction.CallbackContext callbackContext)
        {
            OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
        }

        private void OnPause(InputAction.CallbackContext objCallbackContext)
        {
            OnPauseAction?.Invoke(this, EventArgs.Empty);
        }

        public Vector2 GetMovementVectorNormalized()
        {
            var inputVector = _playerInputActions.Player.Move.ReadValue<Vector2>();
            inputVector = inputVector.normalized;
            return inputVector;
        }

        public string GetControlBindingsText(ControlBindings controlBindings)
        {
            switch (controlBindings)
            {
                default:
                case ControlBindings.MoveUp:
                    return _playerInputActions.Player.Move.bindings[1].ToDisplayString();
                case ControlBindings.MoveDown:
                    return _playerInputActions.Player.Move.bindings[2].ToDisplayString();
                case ControlBindings.MoveLeft:
                    return _playerInputActions.Player.Move.bindings[3].ToDisplayString();
                case ControlBindings.MoveRight:
                    return _playerInputActions.Player.Move.bindings[4].ToDisplayString();
                case ControlBindings.Interact:
                    return _playerInputActions.Player.Interact.bindings[0].ToDisplayString();
                case ControlBindings.InteractAlternate:
                    return _playerInputActions.Player.InteractAlternate.bindings[0].ToDisplayString();
                case ControlBindings.Pause:
                    return _playerInputActions.Player.Pause.bindings[0].ToDisplayString();
            }
        }

        public void RebindControlBinding(ControlBindings controlBindings, Action onActionRebound)
        {
            _playerInputActions.Player.Disable();

            InputAction inputAction;
            int bindingIndex;

            switch (controlBindings)
            {
                default:
                case ControlBindings.MoveUp:
                    inputAction = _playerInputActions.Player.Move;
                    bindingIndex = 1;
                    break;
                case ControlBindings.MoveDown:
                    inputAction = _playerInputActions.Player.Move;
                    bindingIndex = 2;
                    break;
                case ControlBindings.MoveLeft:
                    inputAction = _playerInputActions.Player.Move;
                    bindingIndex = 3;
                    break;
                case ControlBindings.MoveRight:
                    inputAction = _playerInputActions.Player.Move;
                    bindingIndex = 4;
                    break;
                case ControlBindings.Interact:
                    inputAction = _playerInputActions.Player.Interact;
                    bindingIndex = 0;
                    break;
                case ControlBindings.InteractAlternate:
                    inputAction = _playerInputActions.Player.InteractAlternate;
                    bindingIndex = 0;
                    break;
                case ControlBindings.Pause:
                    inputAction = _playerInputActions.Player.Pause;
                    bindingIndex = 0;
                    break;
            }

            inputAction.PerformInteractiveRebinding(bindingIndex).OnComplete(callback =>
                {
                    callback.Dispose();
                    _playerInputActions.Player.Enable();
                    onActionRebound();
                    
                    var savedInputs = _playerInputActions.SaveBindingOverridesAsJson();
                    PlayerPrefs.SetString(PLAYER_PREFS_BINDINGS, savedInputs);
                })
                .Start();
        }

        #region OldInputSystem

        /*private void LegacyInput()
        {
            var inputVector = new Vector2(0, 0);
            
            if (Input.GetKey(KeyCode.W))
            {
                inputVector.y = + 1;
            }
            if (Input.GetKey(KeyCode.S))
            {
                inputVector.y = -1;
            }
            if (Input.GetKey(KeyCode.A))
            {
                inputVector.x = -1;
            }
            if (Input.GetKey(KeyCode.D))
            {
                inputVector.x = +1;
            }
        }*/

        #endregion
    }
}