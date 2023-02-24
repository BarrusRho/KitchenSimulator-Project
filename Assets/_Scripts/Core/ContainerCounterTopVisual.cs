using System;
using UnityEngine;

namespace KitchenSimulator.Core
{
    public class ContainerCounterTopVisual : MonoBehaviour
    {
        private Animator _animator;
        [SerializeField] private ContainerCounterTop _containerCounterTop;

        private const string OPEN_CLOSE = "OpenClose";

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            _containerCounterTop.OnPlayerGrabbedIngredient += OnIngredientGrabbed;
        }

        private void OnIngredientGrabbed(object sender, EventArgs eventArgs)
        {
            _animator.SetTrigger(OPEN_CLOSE);
        }
    }
}
