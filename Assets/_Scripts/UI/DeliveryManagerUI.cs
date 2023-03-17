using System;
using KitchenSimulator.Management;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace KitchenSimulator.UI
{
    public class DeliveryManagerUI : MonoBehaviour
    {
        [SerializeField] private Transform _recipeContainer;
        [SerializeField] private Transform _recipeTemplate;

        private void Awake()
        {
            _recipeTemplate.gameObject.SetActive(false);
        }

        private void Start()
        {
            DeliveryManager.Instance.OnRecipeSpawned += OnRecipeSpawned;
            DeliveryManager.Instance.OnRecipeCompleted += OnRecipeCompleted;
            
            UpdateVisuals();
        }

        private void OnRecipeSpawned(object sender, EventArgs eventArgs)
        {
            UpdateVisuals();
        }

        private void OnRecipeCompleted(object sender, EventArgs eventArgs)
        {
            UpdateVisuals();
        }
        
        private void UpdateVisuals()
        {
            foreach (Transform recipeTemplate in _recipeContainer)
            {
                if (recipeTemplate == _recipeTemplate)
                {
                    continue;
                }

                Destroy(recipeTemplate.gameObject);
            }

            foreach (var recipeSO in DeliveryManager.Instance.GetWaitingRecipeSOList())
            {
                var recipeTransform = Instantiate(_recipeTemplate, _recipeContainer);
                recipeTransform.gameObject.SetActive(true);
                recipeTransform.GetComponent<DeliveryManagerSingleUI>().SetRecipeSO(recipeSO);
            }
        }
    }
}