using KitchenSimulator.ScriptableObjects;
using UnityEngine;
using UnityEngine.Serialization;

namespace KitchenSimulator.Core
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
                    
                }
                else
                {
                    GetIngredient().SetIngredientParent(player);
                }
            }
        }
    }
}