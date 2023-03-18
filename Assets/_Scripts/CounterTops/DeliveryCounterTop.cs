using System;
using KitchenSimulator.Core;
using KitchenSimulator.CounterTops;
using KitchenSimulator.Management;
using UnityEngine;

namespace KitchenSimulator.CounterTops
{
    public class DeliveryCounterTop : CounterTopBase
    {
        //TODO get rid of this Singleton and replace with sender as DeliveryCounterTop
        public static DeliveryCounterTop Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        public override void Interact(Player player)
        {
            if (player.HasIngredient())
            {
                if (player.GetIngredient().TryGetPlate(out Plate plate))
                {
                    DeliveryManager.Instance.DeliverRecipe(plate);
                    player.GetIngredient().DestroySelf();
                }
            }
        }
    }
}
