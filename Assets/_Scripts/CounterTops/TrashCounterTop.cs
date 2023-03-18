using System;
using KitchenSimulator.Core;
using UnityEngine;

namespace KitchenSimulator.CounterTops
{
    public class TrashCounterTop : CounterTopBase
    {
        public static EventHandler OnAnyObjectTrashed;
        
        public override void Interact(Player player)
        {
            if (player.HasIngredient())
            {
                player.GetIngredient().DestroySelf();
                OnAnyObjectTrashed?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
