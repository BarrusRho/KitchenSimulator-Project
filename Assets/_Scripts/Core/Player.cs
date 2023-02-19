using System;
using KitchenSimulator.Management;
using UnityEngine;

namespace KitchenSimulator.Core
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private InputManager _inputManager;
        
        [SerializeField] private float _moveSpeed;
        public float MoveSpeed => _moveSpeed;

        private bool _isWalking;

        private void Awake()
        {
            _inputManager = FindObjectOfType<InputManager>();
        }

        private void Update()
        {
            var inputVector = _inputManager.GetMovementVectorNormalized();
            var moveDirection = new Vector3(inputVector.x, 0f, inputVector.y);
            var thisTransform = transform;
            
            thisTransform.position += moveDirection * (_moveSpeed * Time.deltaTime);
            _isWalking = moveDirection != Vector3.zero;

            var rotateSpeed = 10f;
            thisTransform.forward = Vector3.Slerp(thisTransform.forward, moveDirection,  rotateSpeed * Time.deltaTime);
        }

        public bool IsWalking()
        {
            return _isWalking;
        }
    }
}