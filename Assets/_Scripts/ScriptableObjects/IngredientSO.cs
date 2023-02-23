using UnityEngine;

namespace KitchenSimulator.ScriptableObjects
{
    [CreateAssetMenu()]
    public class IngredientSO : ScriptableObject
    {
        public Transform ingredientPrefab;
        public Sprite spriteIcon;
        public string ingredientName;
    }
}

