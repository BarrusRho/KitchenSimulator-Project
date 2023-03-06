using UnityEngine;

namespace KitchenSimulator.ScriptableObjects
{
    [CreateAssetMenu()]
    public class BurningRecipeSO : ScriptableObject
    {
        public IngredientSO inputIngredient;
        public IngredientSO outputIngredient;

        public float burningProgressMaximum;
    }
}
