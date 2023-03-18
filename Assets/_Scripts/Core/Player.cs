using System;
using KitchenSimulator.CounterTops;
using KitchenSimulator.Management;
using UnityEngine;

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
        private CounterTopBase _selectedCounterTop;
        [SerializeField] private Transform _ingredientHoldPoint;
        private Ingredient _ingredient;

        public float MoveSpeed => _moveSpeed;

        public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
        public event EventHandler OnPickedUpObject;

        public class OnSelectedCounterChangedEventArgs : EventArgs
        {
            public CounterTopBase SelectedCounterTop;
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
            _inputManager.OnInteractAlternateAction += OnInteractAlternatePerformed;
        }

        private void Update()
        {
            HandleMovement();
            HandleInteractions();
        }

        private void OnInteractPerformed(object sender, EventArgs eventArguments)
        {
            if (!GameManager.Instance.IsGamePlaying())
            {
                return;
            }
            
            if (_selectedCounterTop != null)
            {
                _selectedCounterTop.Interact(this);
            }
        }

        private void OnInteractAlternatePerformed(object sender, EventArgs eventArguments)
        {
            if (!GameManager.Instance.IsGamePlaying())
            {
                return;
            }
            
            if (_selectedCounterTop != null)
            {
                _selectedCounterTop.InteractAlternate(this);
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
                canMove = moveDirection.x != 0 && !Physics.CapsuleCast(thisPosition,
                    thisPosition + Vector3.up * playerHeight, playerRadius, moveDirectionX, moveDistance);
                if (canMove)
                {
                    moveDirection = moveDirectionX;
                }
                else
                {
                    var moveDirectionZ = new Vector3(0, 0, moveDirection.z).normalized;
                    canMove = moveDirection.z != 0 && !Physics.CapsuleCast(thisPosition,
                        thisPosition + Vector3.up * playerHeight, playerRadius, moveDirectionZ, moveDistance);

                    if (canMove)
                    {
                        moveDirection = moveDirectionZ;
                    }
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
                if (raycastHit.transform.TryGetComponent(out CounterTopBase counterTopBase))
                {
                    if (counterTopBase != _selectedCounterTop)
                    {
                        SetSelectedCounter(counterTopBase);
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

        private void SetSelectedCounter(CounterTopBase selectedCounterTop)
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

            if (ingredient != null)
            {
                OnPickedUpObject?.Invoke(this, EventArgs.Empty);
            }
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