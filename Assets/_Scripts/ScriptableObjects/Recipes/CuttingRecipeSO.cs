using UnityEngine;

namespace KitchenSimulator.ScriptableObjects
{
    [CreateAssetMenu()]
    public class CuttingRecipeSO : ScriptableObject
    {
        public IngredientSO inputIngredient;
        public IngredientSO outputIngredient;
    }
}
