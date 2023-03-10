using System;
using KitchenSimulator.Core;
using UnityEngine;

namespace KitchenSimulator.UI
{
    public class PlateIconsUI : MonoBehaviour
    {
        [SerializeField] private Plate _plate;
        [SerializeField] private Transform _iconContainer;

        private void Awake()
        {
            _iconContainer.gameObject.SetActive(false);
        }

        private void Start()
        {
            _plate.OnIngredientAdded += OnIngredientAdded;
        }

        private void OnIngredientAdded(object sender, Plate.OnIngredientAddedEventArgs eventArgs)
        {
            UpdatePlateVisuals();
        }

        private void UpdatePlateVisuals()
        {
            foreach (Transform childObject in this.transform)
            {
                if (childObject == _iconContainer)
                {
                    continue;
                }
                Destroy(childObject.gameObject);
            }
            
            foreach (var ingredientSO in _plate.GetIngredientSOList())
            {
                var iconTransform = Instantiate(_iconContainer, this.transform);
                iconTransform.gameObject.SetActive(true);
                iconTransform.GetComponent<PlateIconsSingleUI>().SetIngredientSO(ingredientSO);
            }
        }
    }
}
