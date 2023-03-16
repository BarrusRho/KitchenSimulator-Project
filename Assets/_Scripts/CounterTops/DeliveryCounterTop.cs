using KitchenSimulator.Core;
using KitchenSimulator.CounterTops;
using KitchenSimulator.Management;
using UnityEngine;

namespace KitchenSimulator.CounterTops
{
    public class DeliveryCounterTop : CounterTopBase
    {
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
