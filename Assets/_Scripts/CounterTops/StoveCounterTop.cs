using System;
using KitchenSimulator.Core;
using KitchenSimulator.ScriptableObjects;
using UnityEngine;

namespace KitchenSimulator.CounterTops
{
    public class StoveCounterTop : CounterTopBase
    {
        [SerializeField] private FryingRecipeSO[] _fryingRecipeSOArray;
        private FryingRecipeSO _fryingRecipeSO;
        private float _fryingProgress;

        private void Update()
        {
            if (HasIngredient())
            {
                _fryingProgress += Time.deltaTime;

                if (_fryingProgress > _fryingRecipeSO.fryingProgressMaximum)
                {
                    _fryingProgress = 0f;
                    Debug.Log("Meat fried");
                    GetIngredient().DestroySelf();
                    Ingredient.SpawnIngredient(_fryingRecipeSO.outputIngredient, this);
                }
            }
            Debug.Log(_fryingProgress);
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
    }
}
