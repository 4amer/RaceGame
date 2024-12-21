using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Windows
{
    public class StartWindow : BaseWindow
    {
        public Action GameStarted;

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
