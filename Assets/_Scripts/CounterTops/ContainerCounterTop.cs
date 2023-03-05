using System;
using KitchenSimulator.Core;
using KitchenSimulator.ScriptableObjects;
using UnityEngine;

namespace KitchenSimulator.CounterTops
{
    public class ContainerCounterTop : CounterTopBase
    {
        [SerializeField] private IngredientSO _ingredientSO;

        public event EventHandler OnPlayerGrabbedIngredient;

        public override void Interact(Player player)
        {
            if (!player.HasIngredient())
            {
                Ingredient.SpawnIngredient(_ingredientSO, player);
                OnPlayerGrabbedIngredient?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}