using System;
using KitchenSimulator.Core;
using KitchenSimulator.ScriptableObjects;
using KitchenSimulator.UI;
using UnityEngine;

namespace KitchenSimulator.CounterTops
{
    public class CuttingCounterTop : CounterTopBase, IHasProgress
    {
        [SerializeField] private CuttingRecipeSO[] _cuttingRecipeSOArray;
        private int _cuttingProgress;

        public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

        public event EventHandler OnCut;
        public static event EventHandler OnAnyCut;

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
                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs()
                        {
                            progressNormalized = (float)_cuttingProgress / cuttingRecipeSO.cuttingProgressMaximum
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
                    if (player.GetIngredient().TryGetPlate(out Plate plate))
                    {
                        if (plate.TryAddIngredientToPlate(GetIngredient().GetIngredientSO()))
                        {
                            GetIngredient().DestroySelf();
                            
                        }
                    }
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
                OnAnyCut?.Invoke(this, EventArgs.Empty);
                
                var cuttingRecipeSO = GetCuttingRecipe(GetIngredient().GetIngredientSO());
                
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs()
                {
                    progressNormalized = (float)_cuttingProgress / cuttingRecipeSO.cuttingProgressMaximum
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