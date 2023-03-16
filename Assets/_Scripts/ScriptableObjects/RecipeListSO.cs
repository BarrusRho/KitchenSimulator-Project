using System.Collections.Generic;
using UnityEngine;

namespace KitchenSimulator.ScriptableObjects
{
    //[CreateAssetMenu()]
    public class RecipeListSO : ScriptableObject
    {
        public List<RecipeSO> recipeSOList;
    }
}