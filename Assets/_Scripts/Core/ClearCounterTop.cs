using KitchenSimulator.ScriptableObjects;
using UnityEngine;
using UnityEngine.Serialization;

namespace KitchenSimulator.Core
{
    public class ClearCounterTop : MonoBehaviour
    {
        [SerializeField] private Transform _counterTopSpawnPoint;
        [SerializeField] private IngredientSO _ingredientSo;
        private Ingredient _ingredient;

        public void Interact()
        {
            if (_ingredient == null)
            {
                var ingredientTransform = Instantiate(_ingredientSo.ingredientPrefab, _counterTopSpawnPoint);
                ingredientTransform.GetComponent<Ingredient>().SetClearCounterTop(this);
            }
            else
            {
                Debug.Log(_ingredient.GetClearCounterTop());
            }
        }

        public Transform GetIngredientFollowTransform()
        {
            return _counterTopSpawnPoint;
        }

        public void SetIngredient(Ingredient ingredient)
        {
            this._ingredient = ingredient;
        }

        public Ingredient GetIngredient()
        {
            return _ingredient;
        }

        public void ClearIngredient()
        {
            _ingredient = null;
        }

        public bool HasIngredient()
        {
            return _ingredient != null;
        }
    }
}