using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenSimulator.Utility
{
    public class SceneLoaderCallback : MonoBehaviour
    {
        private bool _isFirstUpdate = true;

        private void Update()
        {
            if (_isFirstUpdate)
            {
                _isFirstUpdate = false;
                
                SceneLoader.SceneLoaderCallback();
            }
        }
    }
}
