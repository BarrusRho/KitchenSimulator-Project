using System.Collections.Generic;
using UnityEngine;

namespace KitchenSimulator.ScriptableObjects
{
    [CreateAssetMenu()]
    public class RecipeSO : ScriptableObject
    {
        public List<IngredientSO> ingredientSOList;
        public string recipeName;
    }
}