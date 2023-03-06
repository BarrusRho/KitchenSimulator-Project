using System;
using KitchenSimulator.Core;
using KitchenSimulator.ScriptableObjects;
using KitchenSimulator.UI;
using UnityEngine;

namespace KitchenSimulator.CounterTops
{
    public class StoveCounterTop : CounterTopBase, IHasProgress
    {
        public enum FryingState
        {
            Idle,
            Frying,
            Fried,
            Burned
        }
        private FryingState _fryingState;

        [SerializeField] private FryingRecipeSO[] _fryingRecipeSOArray;
        [SerializeField] private BurningRecipeSO[] _burningRecipeSOArray;
        private FryingRecipeSO _fryingRecipeSO;
        private BurningRecipeSO _burningRecipeSO;
        private float _fryingProgress;
        private float _burningProgress;
        
        public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

        public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
        public class OnStateChangedEventArgs : EventArgs
        {
            public FryingState fryingState;
        }

        private void Start()
        {
            _fryingState = FryingState.Idle;
        }

        private void Update()
        {
            if (HasIngredient())
            {
                switch (_fryingState)
                {
                    case FryingState.Idle:
                        break;
                    case FryingState.Frying:
                        _fryingProgress += Time.deltaTime;
                        
                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs()
                        {
                            progressNormalized = _fryingProgress / _fryingRecipeSO.fryingProgressMaximum
                        });

                        if (_fryingProgress > _fryingRecipeSO.fryingProgressMaximum)
                        {
                            GetIngredient().DestroySelf();
                            Ingredient.SpawnIngredient(_fryingRecipeSO.outputIngredient, this);
                            _fryingState = FryingState.Fried;
                            _burningProgress = 0f;
                            _burningRecipeSO = GetBurningRecipe(GetIngredient().GetIngredientSO());
                            
                            OnStateChanged?.Invoke(this, new OnStateChangedEventArgs()
                            {
                                fryingState = _fryingState
                            });
                        }

                        break;
                    case FryingState.Fried:
                        _burningProgress += Time.deltaTime;
                        
                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs()
                        {
                            progressNormalized = _burningProgress / _burningRecipeSO.burningProgressMaximum
                        });

                        if (_burningProgress > _burningRecipeSO.burningProgressMaximum)
                        {
                            GetIngredient().DestroySelf();
                            Ingredient.SpawnIngredient(_burningRecipeSO.outputIngredient, this);
                            _fryingState = FryingState.Burned;
                            
                            OnStateChanged?.Invoke(this, new OnStateChangedEventArgs()
                            {
                                fryingState = _fryingState
                            });
                            
                            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs()
                            {
                                progressNormalized = 0f
                            });
                        }

                        break;
                    case FryingState.Burned:
                        break;
                }
            }
        }

        public override void Interact(Player player)
        {
            if (!HasIngredient())
            {
                if (player.HasIngredient())
                {
                    if (HasValidRecipe(player.GetIngredient().GetIngredientSO()))
                    {
                        player.GetIngredient().SetIngredientParent(this);
                        _fryingRecipeSO = GetFryingRecipe(GetIngredient().GetIngredientSO());
                        _fryingState = FryingState.Frying;
                        _fryingProgress = 0f;
                        
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs()
                        {
                            fryingState = _fryingState
                        });
                        
                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs()
                        {
                            progressNormalized = _fryingProgress / _fryingRecipeSO.fryingProgressMaximum
                        });
                    }
                }
                else
                {
                }
            }
            else
            {
                if (player.HasIngredient())
                {
                }
                else
                {
                    GetIngredient().SetIngredientParent(player);
                    _fryingState = FryingState.Idle;
                    
                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs()
                    {
                        fryingState = _fryingState
                    });
                    
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs()
                    {
                        progressNormalized = 0f
                    });
                }
            }
        }

        private bool HasValidRecipe(IngredientSO inputIngredient)
        {
            var fryingRecipeSO = GetFryingRecipe(inputIngredient);
            return fryingRecipeSO != null;
        }

        private IngredientSO GetIngredientOutput(IngredientSO inputIngredient)
        {
            var fryingRecipeSO = GetFryingRecipe(inputIngredient);

            if (fryingRecipeSO != null)
            {
                return fryingRecipeSO.outputIngredient;
            }
            else
            {
                return null;
            }
        }

        private FryingRecipeSO GetFryingRecipe(IngredientSO inputIngredient)
        {
            foreach (var fryingRecipeSO in _fryingRecipeSOArray)
            {
                if (fryingRecipeSO.inputIngredient == inputIngredient)
                {
                    return fryingRecipeSO;
                }
            }

            return null;
        }

        private BurningRecipeSO GetBurningRecipe(IngredientSO inputIngredient)
        {
            foreach (var burningRecipeSO in _burningRecipeSOArray)
            {
                if (burningRecipeSO.inputIngredient == inputIngredient)
                {
                    return burningRecipeSO;
                }
            }

            return null;
        }
    }
}