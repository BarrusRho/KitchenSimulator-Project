using System;
using KitchenSimulator.Management;
using UnityEngine;
using UnityEngine.Serialization;

namespace KitchenSimulator.Core
{
    public class Player : MonoBehaviour, IIngredientParent
    {
        public static Player Instance { get; private set; }

        [SerializeField] private InputManager _inputManager;

        [SerializeField] private float _moveSpeed;
        [SerializeField] private LayerMask _countersLayermask;
        private Vector3 _lastInteractDirection;
        private bool _isWalking;
        private ClearCounterTop _selectedCounterTop;
        [SerializeField] private Transform _ingredientHoldPoint;
        private Ingredient _ingredient;

        public float MoveSpeed => _moveSpeed;

        public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;

        public class OnSelectedCounterChangedEventArgs : EventArgs
        {
            public ClearCounterTop SelectedCounterTop;
        }

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError($"There is more than one Player instance");
            }

            Instance = this;
            _inputManager = FindObjectOfType<InputManager>();
        }

        private void Start()
        {
            _inputManager.OnInteractAction += OnInteractPerformed;
        }

        private void Update()
        {
            HandleMovement();
            HandleInteractions();
        }

        private void OnInteractPerformed(object sender, EventArgs eventArguments)
        {
            if (_selectedCounterTop != null)
            {
                _selectedCounterTop.Interact(this);
            }
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

            if (Physics.Raycast(transform.position, _lastInteractDirection, out var raycastHit, interactDistance,
                    _countersLayermask))
            {
                if (raycastHit.transform.TryGetComponent(out ClearCounterTop clearCounter))
                {
                    if (clearCounter != _selectedCounterTop)
                    {
                        SetSelectedCounter(clearCounter);
                    }
                }
                else
                {
                    SetSelectedCounter(null);
                }
            }
            else
            {
                SetSelectedCounter(null);
            }
        }

        private void SetSelectedCounter(ClearCounterTop selectedCounterTop)
        {
            this._selectedCounterTop = selectedCounterTop;
            OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs()
            {
                SelectedCounterTop = _selectedCounterTop
            });
        }

        public bool IsWalking()
        {
            return _isWalking;
        }

        public Transform GetIngredientFollowTransform()
        {
            return _ingredientHoldPoint;
        }

        public void SetIngredient(Ingredient ingredient)
        {
            this._ingredient = ingredient;
        }

        public Ingredient GetIngredient()
        {
            return _ingredient;
        }

        public void ClearIngredient()
        {
            _ingredient = null;
        }

        public bool HasIngredient()
        {
            return _ingredient != null;
        }
    }
}