using KitchenSimulator.ScriptableObjects;
using UnityEngine;

namespace KitchenSimulator.Core
{
    public class Ingredient : MonoBehaviour
    {
        [SerializeField] private IngredientSO _ingredientSO;
        private IIngredientParent _ingredientParent;

        public IngredientSO GetIngredientSo()
        {
            return _ingredientSO;
        }

        public void SetIngredientParent(IIngredientParent ingredientParent)
        {
            if (this._ingredientParent != null)
            {
                this._ingredientParent.ClearIngredient();
            }

            this._ingredientParent = ingredientParent;

            if (ingredientParent.HasIngredient())
            {
                Debug.Log($"IngredientParent already has an ingredient");
            }

            ingredientParent.SetIngredient(this);

            transform.parent = ingredientParent.GetIngredientFollowTransform();
            transform.localPosition = Vector3.zero;
        }

        public IIngredientParent GetIngredientParent()
        {
            return _ingredientParent;
        }
    }
}