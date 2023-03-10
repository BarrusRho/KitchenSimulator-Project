using System;
using System.Collections.Generic;
using KitchenSimulator.ScriptableObjects;
using UnityEngine;

namespace KitchenSimulator.Core
{
    public class Plate : Ingredient
    {
        [SerializeField] private List<IngredientSO> _validIngredientsList;
        private List<IngredientSO> _ingredientSOList;

        public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;

        public class OnIngredientAddedEventArgs : EventArgs
        {
            public IngredientSO ingredientSO;
        }

        private void Awake()
        {
            _ingredientSOList = new List<IngredientSO>();
        }

        public bool TryAddIngredientToPlate(IngredientSO ingredient)
        {
            if (!_validIngredientsList.Contains(ingredient))
            {
                return false;
            }
            
            if (_ingredientSOList.Contains(ingredient))
            {
                return false;
            }
            else
            {
                _ingredientSOList.Add(ingredient);
                OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs()
                {
                    ingredientSO = ingredient
                });
                return true;
            }
        }
    }
}