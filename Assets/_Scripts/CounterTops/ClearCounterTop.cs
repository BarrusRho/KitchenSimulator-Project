using KitchenSimulator.Core;
using KitchenSimulator.ScriptableObjects;
using UnityEditor;
using UnityEngine;

namespace KitchenSimulator.CounterTops
{
    public class ClearCounterTop : CounterTopBase
    {
        [SerializeField] private IngredientSO _ingredientSo;

        public override void Interact(Player player)
        {
            if (!HasIngredient())
            {
                if (player.HasIngredient())
                {
                    player.GetIngredient().SetIngredientParent(this);
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
                    else
                    {
                        if (GetIngredient().TryGetPlate(out plate))
                        {
                            if (plate.TryAddIngredientToPlate(player.GetIngredient().GetIngredientSO()))
                            {
                                player.GetIngredient().DestroySelf();
                            }
                        }
                    }
                }
                else
                {
                    GetIngredient().SetIngredientParent(player);
                }
            }
        }
    }
}