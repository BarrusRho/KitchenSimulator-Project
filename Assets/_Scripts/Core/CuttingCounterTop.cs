using KitchenSimulator.ScriptableObjects;
using UnityEngine;

namespace KitchenSimulator.Core
{
    public class CuttingCounterTop : CounterTopBase
    {
        [SerializeField] private IngredientSO _cutIngredientSO;
        
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

        public override void InteractAlternate(Player player)
        {
            if (HasIngredient())
            {
                GetIngredient().DestroySelf();
                Ingredient.SpawnIngredient(_cutIngredientSO, this);
            }
        }
    }
}
