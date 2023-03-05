using KitchenSimulator.Core;
using UnityEngine;

namespace KitchenSimulator.CounterTops
{
    public class CounterTopBase : MonoBehaviour, IIngredientParent
    {
        [SerializeField] private Transform _counterTopSpawnPoint;
        private Ingredient _ingredient;
        
        public virtual void Interact(Player player)
        {
            
        }

        public virtual void InteractAlternate(Player player)
        {
            
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
