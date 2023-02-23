using KitchenSimulator.ScriptableObjects;
using UnityEngine;

namespace KitchenSimulator.Core
{
    public class Ingredient : MonoBehaviour
    {
        [SerializeField] private IngredientSO _ingredientSO;
        private ClearCounterTop _clearCounterTop;

        public IngredientSO GetIngredientSo()
        {
            return _ingredientSO;
        }

        public void SetClearCounterTop(ClearCounterTop clearCounterTop)
        {
            if (this._clearCounterTop !=null)
            {
                this._clearCounterTop.ClearIngredient();
            }
            this._clearCounterTop = clearCounterTop;

            if (clearCounterTop.HasIngredient())
            {
                Debug.Log($"CounterTop already has an ingredient!");
            }
            clearCounterTop.SetIngredient(this);
            
            transform.parent = clearCounterTop.GetIngredientFollowTransform();
            transform.localPosition = Vector3.zero;
        }

        public ClearCounterTop GetClearCounterTop()
        {
            return _clearCounterTop;
        }
    }
}
