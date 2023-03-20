using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace KitchenSimulator.Utility
{
    public static class SceneLoader
    {
        public enum Scene
        {
            MainMenuScene,
            GameScene,
            LoadingScene
        }
        
        private static Scene _targetScene;

        public static void LoadScene(Scene targetScene)
        {
            SceneLoader._targetScene = targetScene;
            SceneManager.LoadScene(Scene.LoadingScene.ToString());
        }

        public static void SceneLoaderCallback()
        {
            SceneManager.LoadScene(_targetScene.ToString());
        }
    }
}
