using System;
using KitchenSimulator.Management;
using UnityEngine;
using UnityEngine.UI;

namespace KitchenSimulator.UI
{
    public class GameClockUI : MonoBehaviour
    {
        [SerializeField] private Image _gameClockTimerImage;
        
        private void Update()
        {
            _gameClockTimerImage.fillAmount = GameManager.Instance.GetGamePlayingTimerNormalized();
        }
    }
}
