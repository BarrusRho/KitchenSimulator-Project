using KitchenSimulator.ScriptableObjects;
using UnityEngine;
using UnityEngine.Serialization;

namespace KitchenSimulator.Core
{
    public class ClearCounterTop : MonoBehaviour, IIngredientParent
    {
        [SerializeField] private Transform _counterTopSpawnPoint;
        [SerializeField] private IngredientSO _ingredientSo;
        private Ingredient _ingredient;

        public void Interact(Player player)
        {
            if (_ingredient == null)
            {
                var ingredientTransform = Instantiate(_ingredientSo.ingredientPrefab, _counterTopSpawnPoint);
                ingredientTransform.GetComponent<Ingredient>().SetIngredientParent(this);
            }
            else
            {
                _ingredient.SetIngredientParent(player);
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