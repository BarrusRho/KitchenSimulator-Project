using UnityEngine;

namespace KitchenSimulator.Core
{
    public interface IIngredientParent
    {
        public Transform GetIngredientFollowTransform();

        public void SetIngredient(Ingredient ingredient);

        public Ingredient GetIngredient();

        public void ClearIngredient();

        public bool HasIngredient();
    }
}