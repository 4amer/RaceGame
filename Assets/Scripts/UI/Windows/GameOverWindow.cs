using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.Windows
{
    public class GameOverWindow : BaseWindow
    {
        public Action RestartLevel;

        public void RestartButtonPresed()
        {
            RestartLevel?.Invoke();
        }

        private void OnDestroy()
        {
            RestartLevel = null;
        }
    }
}
