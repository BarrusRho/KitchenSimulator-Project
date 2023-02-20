using System;
using KitchenSimulator.Management;
using UnityEngine;

namespace KitchenSimulator.Core
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private InputManager _inputManager;

        [SerializeField] private float _moveSpeed;
        [SerializeField] private LayerMask _countersLayermask;
        public float MoveSpeed => _moveSpeed;

        private Vector3 _lastInteractDirection;
        private bool _isWalking;

        private void Awake()
        {
            _inputManager = FindObjectOfType<InputManager>();
        }

        private void Update()
        {
            HandleMovement();
            HandleInteractions();
        }

        private void HandleMovement()
        {
            var inputVector = _inputManager.GetMovementVectorNormalized();
            var moveDirection = new Vector3(inputVector.x, 0f, inputVector.y);
            var thisTransform = transform;
            var thisPosition = transform.position;
            var playerRadius = 0.7f;
            var playerHeight = 2f;
            var moveDistance = _moveSpeed * Time.deltaTime;
            var canMove = !Physics.CapsuleCast(thisPosition,
                thisPosition + Vector3.up * playerHeight, playerRadius, moveDirection, moveDistance);

            if (!canMove)
            {
                var moveDirectionX = new Vector3(moveDirection.x, 0, 0).normalized;
                canMove = !Physics.CapsuleCast(thisPosition,
                    thisPosition + Vector3.up * playerHeight, playerRadius, moveDirectionX, moveDistance);
                if (canMove)
                {
                    moveDirection = moveDirectionX;
                }
                else
                {
                    var moveDirectionZ = new Vector3(0, 0, moveDirection.z).normalized;
                    canMove = !Physics.CapsuleCast(thisPosition,
                        thisPosition + Vector3.up * playerHeight, playerRadius, moveDirectionZ, moveDistance);
                }

                if (canMove)
                {
                    moveDirection = moveDirectionX;
                }
            }

            if (canMove)
            {
                thisTransform.position += moveDirection * moveDistance;
            }

            _isWalking = moveDirection != Vector3.zero;

            var rotateSpeed = 10f;
            thisTransform.forward = Vector3.Slerp(thisTransform.forward, moveDirection, rotateSpeed * Time.deltaTime);
        }

        private void HandleInteractions()
        {
            var inputVector = _inputManager.GetMovementVectorNormalized();
            var moveDirection = new Vector3(inputVector.x, 0f, inputVector.y);

            if (moveDirection != Vector3.zero)
            {
                _lastInteractDirection = moveDirection;
            }
            
            var interactDistance = 2f;

            if (Physics.Raycast(transform.position, _lastInteractDirection, out var raycastHit, interactDistance, _countersLayermask))
            {
                if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
                {
                    clearCounter.Interact();
                }
            }
        }

        public bool IsWalking()
        {
            return _isWalking;
        }
    }
}