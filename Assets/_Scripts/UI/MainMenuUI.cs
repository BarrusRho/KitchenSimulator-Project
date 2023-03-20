using System;
using System.Collections;
using System.Collections.Generic;
using KitchenSimulator.Utility;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace KitchenSimulator.UI
{
    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _quitButton;

        private void Awake()
        {
            _playButton.onClick.AddListener(() =>
            {
                SceneLoader.LoadScene(SceneLoader.Scene.GameScene);
            });
            
            _quitButton.onClick.AddListener(() =>
            {
                Application.Quit();
            });
        }
    }
}
