using UnityEngine;

namespace KitchenSimulator.ScriptableObjects
{
    [CreateAssetMenu()]
    public class FryingRecipeSO : ScriptableObject
    {
        public IngredientSO inputIngredient;
        public IngredientSO outputIngredient;

        public float fryingProgressMaximum;
    }
}
