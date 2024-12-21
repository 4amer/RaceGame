using UI.Windows;
using UnityEngine;

namespace UI.Manager
{
    public class UIManager : MonoBehaviour, IUIManager
    {
        [SerializeField] private BaseWindow[] _windows = null;

        [SerializeField] private StartWindow _startWindow = null;
        [SerializeField] private GameOverWindow _gameOverWindow = null;

        public void ShowWindow(WindowsTypes type)
        {
            foreach (BaseWindow window in _windows)
            {
                if (window.Type == type)
                {
                    window.Show();
                }
                else
                {
                    window.Hide();
                }
            }
        }

        public StartWindow GetStartWidnow { get => _startWindow; }
        public GameOverWindow GetGameOverWindow { get => _gameOverWindow; }
    }

    public interface IUIManager
    {
        public void ShowWindow(WindowsTypes type);
        public StartWindow GetStartWidnow { get; }
        public GameOverWindow GetGameOverWindow { get; }
    }   

    public enum WindowsTypes
    {
        None = 0,
        StartWindow = 1,
        InGameWindow = 2,
        GameOverWidnow = 3,
    }
}
