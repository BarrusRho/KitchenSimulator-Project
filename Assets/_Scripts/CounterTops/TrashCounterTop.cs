using KitchenSimulator.Core;
using UnityEngine;

namespace KitchenSimulator.CounterTops
{
    public class TrashCounterTop : CounterTopBase
    {
        public override void Interact(Player player)
        {
            if (player.HasIngredient())
            {
                player.GetIngredient().DestroySelf();
            }
        }
    }
}
