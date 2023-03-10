using System;
using System.Collections.Generic;
using KitchenSimulator.ScriptableObjects;
using UnityEngine;

namespace KitchenSimulator.Core
{
    public class PlateCompleteVisual : MonoBehaviour
    {
        [Serializable]
        public struct IngredientSOGameObject
        {
            public IngredientSO ingredientSO;
            public GameObject ingredientGameObject;
        }

        [SerializeField] private Plate _plate;
        [SerializeField] private List<IngredientSOGameObject> _ingredientSOGameObjectList;

        private void Start()
        {
            _plate.OnIngredientAdded += OnIngredientAdded;

            foreach (var ingredientGameObject in _ingredientSOGameObjectList)
            {
                ingredientGameObject.ingredientGameObject.SetActive(false);
            }
        }

        private void OnIngredientAdded(object sender, Plate.OnIngredientAddedEventArgs eventArgs)
        {
            foreach (var ingredientGameObject in _ingredientSOGameObjectList)
            {
                if (ingredientGameObject.ingredientSO == eventArgs.ingredientSO)
                {
                    ingredientGameObject.ingredientGameObject.SetActive(true);
                }
            }
        }
    }
}