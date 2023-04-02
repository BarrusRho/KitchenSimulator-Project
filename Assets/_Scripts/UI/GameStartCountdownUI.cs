using System;
using System.Collections;
using System.Collections.Generic;
using KitchenSimulator.Audio;
using KitchenSimulator.Management;
using TMPro;
using UnityEngine;

namespace KitchenSimulator.UI
{
    public class GameStartCountdownUI : MonoBehaviour
    {
        private Animator _animator;

        private const string NUMBER_POPUP = "NumberPopup";
        
        private int _previousCountdownNumber;
        [SerializeField] private TextMeshProUGUI _countdownText;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            GameManager.Instance.OnStateChanged += OnStateChanged;
            
            HideUI();
        }

        private void Update()
        {
            var countdownNumber = Mathf.CeilToInt(GameManager.Instance.GetCountdownToStartTimer());
            _countdownText.text = countdownNumber.ToString();

            if (_previousCountdownNumber != countdownNumber)
            {
                _previousCountdownNumber = countdownNumber;
                _animator.SetTrigger(NUMBER_POPUP);
                AudioManager.Instance.PlayCountdownSoundEffect();
            }
        }

        private void OnStateChanged(object sender, EventArgs eventArgs)
        {
            if (GameManager.Instance.IsCountdownToStartActive())
            {
                ShowUI();
            }
            else
            {
                HideUI();
            }
        }

        private void ShowUI()
        {
            this.gameObject.SetActive(true);
        }

        private void HideUI()
        {
            this.gameObject.SetActive(false);
        }
    }
}
