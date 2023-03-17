using System;
using KitchenSimulator.ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KitchenSimulator.UI
{
    public class DeliveryManagerSingleUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _recipeNameText;
        [SerializeField] private Transform _ingredientIconContainer;
        [SerializeField] private Transform _ingredientIconImage;

        private void Awake()
        {
            _ingredientIconImage.gameObject.SetActive(false);
        }

        public void SetRecipeSO(RecipeSO recipeSO)
        {
            _recipeNameText.text = recipeSO.recipeName;

            foreach (Transform child in _ingredientIconContainer)
            {
                if (child == _ingredientIconImage)
                {
                    continue;
                }
                
                Destroy(child.gameObject);
            }

            foreach (var ingredientSO in recipeSO.ingredientSOList)
            {
                var iconTransform = Instantiate(_ingredientIconImage, _ingredientIconContainer);
                iconTransform.gameObject.SetActive(true);
                iconTransform.GetComponent<Image>().sprite = ingredientSO.spriteIcon;
            }
        }
    }
}
