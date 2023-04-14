using System;
using System.Collections;
using System.Collections.Generic;
using KitchenSimulator.Management;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KitchenSimulator.UI
{
    public class DeliveryResultUI : MonoBehaviour
    {
        private Animator _animator;
        private const string POPUP = "Popup";
        
        [SerializeField] private Image _backgroundImage;
        [SerializeField] private Image _iconImage;
        [SerializeField] private TextMeshProUGUI _messageText;
        [SerializeField] private Color _successColor;
        [SerializeField] private Color _failedColor;
        [SerializeField] private Sprite _successSprite;
        [SerializeField] private Sprite _failedSprite;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            DeliveryManager.Instance.OnRecipeSuccess += OnRecipeSuccess;
            DeliveryManager.Instance.OnRecipeFailed += OnRecipeFailed;

            this.gameObject.SetActive(false);
        }
        
        private void OnRecipeSuccess(object sender, EventArgs eventArgs)
        {
            this.gameObject.SetActive(true);
            _animator.SetTrigger(POPUP);
            _backgroundImage.color = _successColor;
            _iconImage.sprite = _successSprite;
            _messageText.text = $"DELIVERY\nSUCCESS";
        }

        private void OnRecipeFailed(object sender, EventArgs eventArgs)
        {
            this.gameObject.SetActive(true);
            _animator.SetTrigger(POPUP);
            _backgroundImage.color = _failedColor;
            _iconImage.sprite = _failedSprite;
            _messageText.text = $"DELIVERY\nFAILED";
        }
    }
}
