using System;
using System.Collections;
using System.Collections.Generic;
using KitchenSimulator.Core;
using UnityEngine;

namespace KitchenSimulator.Audio
{
    public class PlayerAudio : MonoBehaviour
    {
        private Player _player;
        private float _footstepTimer;
        private float _footstepTimerMaximum = 0.1f;

        public static event EventHandler OnPlayerMovement;

        private void Awake()
        {
            _player = GetComponent<Player>();
        }

        private void Update()
        {
            _footstepTimer -= Time.deltaTime;

            if (_footstepTimer < 0f)
            {
                _footstepTimer = _footstepTimerMaximum;

                if (_player.IsWalking())
                {
                    OnPlayerMovement?.Invoke(this, EventArgs.Empty);
                }
            }
        }
    }
}
