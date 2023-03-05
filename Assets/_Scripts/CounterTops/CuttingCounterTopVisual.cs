using System;
using UnityEngine;

namespace KitchenSimulator.CounterTops
{
    public class CuttingCounterTopVisual : MonoBehaviour
    {
        private Animator _animator;
        [SerializeField] private CuttingCounterTop _cuttingCounterTop;

        private const string CUT = "Cut";

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            _cuttingCounterTop.OnCut += OnCut;
        }

        private void OnCut(object sender, EventArgs eventArgs)
        {
            _animator.SetTrigger(CUT);
        }
    }
}
