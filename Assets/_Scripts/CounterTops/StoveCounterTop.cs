using System;
using KitchenSimulator.Core;
using KitchenSimulator.ScriptableObjects;
using UnityEngine;

namespace KitchenSimulator.CounterTops
{
    public class StoveCounterTop : CounterTopBase
    {
        private enum FryingState
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

                        if (_fryingProgress > _fryingRecipeSO.fryingProgressMaximum)
                        {
                            GetIngredient().DestroySelf();
                            Ingredient.SpawnIngredient(_fryingRecipeSO.outputIngredient, this);
                            _fryingState = FryingState.Fried;
                            _burningProgress = 0f;
                            _burningRecipeSO = GetBurningRecipe(GetIngredient().GetIngredientSO());
                        }
                        break;
                    case FryingState.Fried:
                        _burningProgress += Time.deltaTime;

                        if (_burningProgress > _burningRecipeSO.burningProgressMaximum)
                        {
                            GetIngredient().DestroySelf();
                            Ingredient.SpawnIngredient(_burningRecipeSO.outputIngredient, this);
                            _fryingState = FryingState.Burned;
                        }
                        break;
                    case FryingState.Burned:
                        break;
                }
            }
            Debug.Log(_fryingState);
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