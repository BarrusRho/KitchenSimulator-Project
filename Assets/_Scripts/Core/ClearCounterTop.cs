using KitchenSimulator.ScriptableObjects;
using UnityEngine;

namespace KitchenSimulator.Core
{
    public class ClearCounter : MonoBehaviour
    {
        [SerializeField] private Transform _counterTopSpawnPoint;
        [SerializeField] private KitchenObjectSO _kitchenObjectSO;
        
        public void Interact()
        {
            Debug.Log("Interacting");
            var kitchenObjectTransform = Instantiate(_kitchenObjectSO.prefab, _counterTopSpawnPoint);
            kitchenObjectTransform.localPosition = Vector3.zero;
        }
    }
    
}
