using System;
using System.Collections;
using System.Collections.Generic;
using KitchenSimulator.Management;
using TMPro;
using UnityEngine;

namespace KitchenSimulator.UI
{
    public class GameOverUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _recipesDeliveredText;
        
        private void Start()
        {
            GameManager.Instance.OnStateChanged += OnStateChanged;
            
            HideUI();
        }
        
        private void OnStateChanged(object sender, EventArgs eventArgs)
        {
            if (GameManager.Instance.IsGameOver())
            {
                _recipesDeliveredText.text = DeliveryManager.Instance.GetDeliveredRecipesAmount().ToString();
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
