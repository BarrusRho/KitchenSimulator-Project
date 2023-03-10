using KitchenSimulator.ScriptableObjects;
using UnityEngine;

namespace KitchenSimulator.Core
{
    public class Ingredient : MonoBehaviour
    {
        [SerializeField] private IngredientSO _ingredientSO;
        private IIngredientParent _ingredientParent;

        public IngredientSO GetIngredientSO()
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

        public void DestroySelf()
        {
            _ingredientParent.ClearIngredient();
            Destroy(this.gameObject);
        }
        
        public bool TryGetPlate(out Plate plate)
        {
            if (this is Plate)
            {
                plate = this as Plate;
                return true;
            }
            else
            {
                plate = null;
                return false;
            }
        }

        public static Ingredient SpawnIngredient(IngredientSO ingredientSO, IIngredientParent ingredientParent)
        {
            var ingredientTransform = Instantiate(ingredientSO.ingredientPrefab);
            var ingredientObject = ingredientTransform.GetComponent<Ingredient>();
            ingredientObject.SetIngredientParent(ingredientParent);
            return ingredientObject;
        }
    }
}