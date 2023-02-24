using System;
using KitchenSimulator.ScriptableObjects;
using UnityEngine;

namespace KitchenSimulator.Core
{
    public class ContainerCounterTop : CounterTopBase
    {
        [SerializeField] private IngredientSO _ingredientSo;

        public event EventHandler OnPlayerGrabbedIngredient;

        public override void Interact(Player player)
        {
            if (!HasIngredient())
            {
                var ingredientTransform = Instantiate(_ingredientSo.ingredientPrefab);
                ingredientTransform.GetComponent<Ingredient>().SetIngredientParent(player);
                OnPlayerGrabbedIngredient?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}