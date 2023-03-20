using System;
using System.Collections;
using System.Collections.Generic;
using KitchenSimulator.Audio;
using KitchenSimulator.CounterTops;
using UnityEngine;

namespace KitchenSimulator.Management
{
    public class ResetStaticDataManager : MonoBehaviour
    {
        private void Awake()
        {
            CuttingCounterTop.ResetStaticData();
            CounterTopBase.ResetStaticData();
            TrashCounterTop.ResetStaticData();
            PlayerAudio.ResetStaticData();
        }
    }
}
