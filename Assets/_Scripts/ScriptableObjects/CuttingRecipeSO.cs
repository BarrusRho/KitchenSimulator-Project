using UnityEngine;

namespace KitchenSimulator.ScriptableObjects
{
    [CreateAssetMenu()]
    public class CuttingRecipeSO : ScriptableObject
    {
        public IngredientSO inputIngredient;
        public IngredientSO outputIngredient;

        public int cuttingProgressMaximum;
    }
}
