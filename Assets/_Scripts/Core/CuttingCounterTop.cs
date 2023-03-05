using System;
using KitchenSimulator.ScriptableObjects;
using UnityEngine;

namespace KitchenSimulator.Core
{
    public class CuttingCounterTop : CounterTopBase
    {
        [SerializeField] private CuttingRecipeSO[] _cuttingRecipeSOArray;
        private int _cuttingProgress;

        public event EventHandler<OnCuttingProgressChangedEventArgs> OnCuttingProgressChanged;
        public class OnCuttingProgressChangedEventArgs : EventArgs
        {
            public float cuttingProgressNormalized;
        }

        public event EventHandler OnCut;

        public override void Interact(Player player)
        {
            if (!HasIngredient())
            {
                if (player.HasIngredient())
                {
                    if (HasValidRecipe(player.GetIngredient().GetIngredientSO()))
                    {
                        player.GetIngredient().SetIngredientParent(this);
                        _cuttingProgress = 0;

                        var cuttingRecipeSO = GetCuttingRecipe(GetIngredient().GetIngredientSO());
                        OnCuttingProgressChanged?.Invoke(this, new OnCuttingProgressChangedEventArgs()
                        {
                            cuttingProgressNormalized = (float)_cuttingProgress / cuttingRecipeSO.cuttingProgressMaximum
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
                }
            }
        }

        public override void InteractAlternate(Player player)
        {
            if (HasIngredient() && HasValidRecipe(GetIngredient().GetIngredientSO()))
            {
                _cuttingProgress++;
                
                OnCut?.Invoke(this, EventArgs.Empty);
                
                var cuttingRecipeSO = GetCuttingRecipe(GetIngredient().GetIngredientSO());
                
                OnCuttingProgressChanged?.Invoke(this, new OnCuttingProgressChangedEventArgs()
                {
                    cuttingProgressNormalized = (float)_cuttingProgress / cuttingRecipeSO.cuttingProgressMaximum
                });
                
                if (_cuttingProgress >= cuttingRecipeSO.cuttingProgressMaximum)
                {
                    var ingredientOutput = GetIngredientOutput(GetIngredient().GetIngredientSO());
                    GetIngredient().DestroySelf();
                    Ingredient.SpawnIngredient(ingredientOutput, this);
                }
            }
        }

        private bool HasValidRecipe(IngredientSO inputIngredient)
        {
            var cuttingRecipeSO = GetCuttingRecipe(inputIngredient);
            return cuttingRecipeSO != null;
        }

        private IngredientSO GetIngredientOutput(IngredientSO inputIngredient)
        {
            var cuttingRecipeSO = GetCuttingRecipe(inputIngredient);

            if (cuttingRecipeSO != null)
            {
                return cuttingRecipeSO.outputIngredient;
            }
            else
            {
                return null;
            }
        }

        private CuttingRecipeSO GetCuttingRecipe(IngredientSO inputIngredient)
        {
            foreach (var cuttingRecipeSO in _cuttingRecipeSOArray)
            {
                if (cuttingRecipeSO.inputIngredient == inputIngredient)
                {
                    return cuttingRecipeSO;
                }
            }

            return null;
        }
    }
}