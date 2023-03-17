using System;
using System.Collections.Generic;
using KitchenSimulator.Core;
using KitchenSimulator.ScriptableObjects;
using UnityEngine;
using Random = UnityEngine.Random;

namespace KitchenSimulator.Management
{
    public class DeliveryManager : MonoBehaviour
    {
        public static DeliveryManager Instance { get; private set; }

        [SerializeField] private RecipeListSO _recipeListSO;
        private List<RecipeSO> _waitingRecipeSOList;

        private float _spawnRecipeTimer;
        private float _spawnRecipeTimerMaximum = 4f;

        private int _waitingRecipesMaximum = 4;

        public event EventHandler OnRecipeSpawned;
        public event EventHandler OnRecipeCompleted;

        private void Awake()
        {
            Instance = this;
            
            _waitingRecipeSOList = new List<RecipeSO>();
        }

        private void Update()
        {
            UpdateRecipeList();
        }

        private void UpdateRecipeList()
        {
            _spawnRecipeTimer -= Time.deltaTime;
            if (_spawnRecipeTimer <= 0f)
            {
                _spawnRecipeTimer = _spawnRecipeTimerMaximum;

                if (_waitingRecipeSOList.Count < _waitingRecipesMaximum)
                {
                    var randomRecipe = Random.Range(0, _recipeListSO.recipeSOList.Count);
                    var waitingRecipeSO = _recipeListSO.recipeSOList[randomRecipe];
                    _waitingRecipeSOList.Add(waitingRecipeSO);
                    OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public void DeliverRecipe(Plate plate)
        {
            for (int i = 0; i < _waitingRecipeSOList.Count; i++)
            {
                var waitingRecipeSO = _waitingRecipeSOList[i];

                if (waitingRecipeSO.ingredientSOList.Count == plate.GetIngredientSOList().Count)
                {
                    var plateContentsMatchesRecipe = true;

                    foreach (var ingredientSO in waitingRecipeSO.ingredientSOList)
                    {
                        var ingredientFound = false;

                        foreach (var plateRecipe in plate.GetIngredientSOList())
                        {
                            if (plateRecipe == ingredientSO)
                            {
                                ingredientFound = true;
                                break;
                            }
                        }

                        if (!ingredientFound)
                        {
                            plateContentsMatchesRecipe = false;
                        }
                    }

                    if (plateContentsMatchesRecipe)
                    {
                        _waitingRecipeSOList.RemoveAt(i);
                        OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                        return;
                    }
                }
            }
        }

        public List<RecipeSO> GetWaitingRecipeSOList()
        {
            return _waitingRecipeSOList;
        }
    }
}