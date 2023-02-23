using KitchenSimulator.ScriptableObjects;
using UnityEngine;

namespace KitchenSimulator.Core
{
    public class Ingredient : MonoBehaviour
    {
        [SerializeField] private IngredientSO _ingredientSO;

        public IngredientSO GetIngredientSo()
        {
            return _ingredientSO;
        }
    }
}
