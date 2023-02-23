using KitchenSimulator.ScriptableObjects;
using UnityEngine;
using UnityEngine.Serialization;

namespace KitchenSimulator.Core
{
    public class ClearCounterTop : MonoBehaviour
    {
        [SerializeField] private Transform _counterTopSpawnPoint;
        [FormerlySerializedAs("_kitchenObjectSO")] [SerializeField] private IngredientSO _ingredientSo;
        
        public void Interact()
        {
            Debug.Log("Interacting");
            
            var ingredientTransform = Instantiate(_ingredientSo.ingredientPrefab, _counterTopSpawnPoint);
            ingredientTransform.localPosition = Vector3.zero;
            
            Debug.Log(ingredientTransform.GetComponent<Ingredient>().GetIngredientSo().ingredientName);
        }
    }
    
}
