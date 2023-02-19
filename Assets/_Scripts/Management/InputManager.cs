using System;
using UnityEngine;

namespace KitchenSimulator.Management
{
    public class InputManager : MonoBehaviour
    {
        private PlayerInputActions _playerInputActions;
        
        private void Awake()
        {
            InitializePlayerInputActions();
        }
        
        private void InitializePlayerInputActions()
        {
            _playerInputActions = new PlayerInputActions();
            _playerInputActions.Player.Enable();
        }
        
        public Vector2 GetMovementVectorNormalized()
        {
            var inputVector = _playerInputActions.Player.Move.ReadValue<Vector2>();
            inputVector = inputVector.normalized;
            return inputVector;
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