using System;
using KitchenSimulator.Core;
using KitchenSimulator.ScriptableObjects;
using UnityEngine;

namespace KitchenSimulator.CounterTops
{
    public class PlateCounterTop : CounterTopBase
    {
        [SerializeField] private IngredientSO _plateObjectSO;
        private float _spawnPlateTimer;
        private float _spawnPlateTimerMaximum = 4f;
        private int _platesSpawnedAmount;
        private int _platesSpawnedAmountMaximum = 4;

        public event EventHandler OnPlateSpawned;
        public event EventHandler OnPlateRemoved;

        private void Update()
        {
            _spawnPlateTimer += Time.deltaTime;
            if (_spawnPlateTimer > _spawnPlateTimerMaximum)
            {
                _spawnPlateTimer = 0f;

                if (_platesSpawnedAmount < _platesSpawnedAmountMaximum)
                {
                    _platesSpawnedAmount++;
                    OnPlateSpawned?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public override void Interact(Player player)
        {
            if (!player.HasIngredient())
            {
                if (_platesSpawnedAmount > 0)
                {
                    _platesSpawnedAmount--;
                    Ingredient.SpawnIngredient(_plateObjectSO, player);
                    OnPlateRemoved?.Invoke(this, EventArgs.Empty);
                }
            }
        }
    }
}
