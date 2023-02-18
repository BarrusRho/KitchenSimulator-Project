using System;
using UnityEngine;

namespace KitchenSimulator.Core
{
    public class PlayerAnimator : MonoBehaviour
    {
        private Animator _animator;
        [SerializeField] private Player _player;

        private const string IS_WALKING = "IsWalking";

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            _animator.SetBool(IS_WALKING, _player.IsWalking());
        }
    }
}
