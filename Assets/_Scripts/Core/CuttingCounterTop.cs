using KitchenSimulator.ScriptableObjects;
using UnityEngine;

namespace KitchenSimulator.Core
{
    public class CuttingCounterTop : CounterTopBase
    {
        [SerializeField] private CuttingRecipeSO[] _cuttingRecipeSOArray;

        public override void Interact(Player player)
        {
            if (!HasIngredient())
            {
                if (player.HasIngredient())
                {
                    if (HasValidRecipe(player.GetIngredient().GetIngredientSo()))
                    {
                        player.GetIngredient().SetIngredientParent(this);
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
            if (HasIngredient() && HasValidRecipe(GetIngredient().GetIngredientSo()))
            {
                var ingredientOutput = GetIngredientOutput(GetIngredient().GetIngredientSo());
                GetIngredient().DestroySelf();
                Ingredient.SpawnIngredient(ingredientOutput, this);
            }
        }

        private bool HasValidRecipe(IngredientSO inputIngredient)
        {
            foreach (var cuttingRecipeSO in _cuttingRecipeSOArray)
            {
                if (cuttingRecipeSO.inputIngredient == inputIngredient)
                {
                    return true;
                }
            }

            return false;
        }

        private IngredientSO GetIngredientOutput(IngredientSO inputIngredient)
        {
            foreach (var cuttingRecipeSO in _cuttingRecipeSOArray)
            {
                if (cuttingRecipeSO.inputIngredient == inputIngredient)
                {
                    return cuttingRecipeSO.outputIngredient;
                }
            }

            return null;
        }
    }
}