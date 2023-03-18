using System;
using System.Collections;
using System.Collections.Generic;
using KitchenSimulator.Management;
using TMPro;
using UnityEngine;

namespace KitchenSimulator.UI
{
    public class GameStartCountdownUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _countdownText;

        private void Start()
        {
            GameManager.Instance.OnStateChanged += OnStateChanged;
            
            HideUI();
        }

        private void Update()
        {
            _countdownText.text = Mathf.Ceil(GameManager.Instance.GetCountdownToStartTimer()).ToString();
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
