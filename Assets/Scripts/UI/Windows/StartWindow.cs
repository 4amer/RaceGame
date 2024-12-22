using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows
{
    public class StartWindow : BaseWindow
    {
        [SerializeField] private Image _logoImage = null;
        public Action GameStarted;

        private Sequence _sequence = null;

        private void OnEnable()
        {
            
        }

        public void ScreenTucked()
        {
            GameStarted?.Invoke();
        }

        private void OnDestroy()
        {
            GameStarted = null;
        }
    }
}
