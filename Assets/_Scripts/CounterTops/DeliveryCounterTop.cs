using KitchenSimulator.Core;
using KitchenSimulator.CounterTops;
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
                    player.GetIngredient().DestroySelf();
                }
            }
        }
    }
}
