using System;
using System.Collections;
using System.Collections.Generic;
using KitchenSimulator.Management;
using KitchenSimulator.Utility;
using UnityEngine;
using UnityEngine.UI;

namespace KitchenSimulator.UI
{
    public class PauseMenuUI : MonoBehaviour
    {
        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _mainMenuButton;

        private void Awake()
        {
            _resumeButton.onClick.AddListener(() =>
            {
                GameManager.Instance.TogglePauseGame();
            });
            
            _mainMenuButton.onClick.AddListener(() =>
            {
                SceneLoader.LoadScene(SceneLoader.Scene.MainMenuScene);
            });
        }

        private void Start()
        {
            GameManager.Instance.OnGamePaused += OnGamePaused;
            GameManager.Instance.OnGameResumed += OnGameResumed;
            
            HideUI();
        }

        private void OnGamePaused(object sender, EventArgs eventArgs)
        {
            ShowUI();
        }

        private void OnGameResumed(object sender, EventArgs eventArgs)
        {
            HideUI();
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
