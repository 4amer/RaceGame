using Game.Input.Manager;
using Game.Obstacle.Manager;
using Game.Player.Manager;
using Game.Road.Manager;
using UI.Manager;
using UI.Windows;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Manager
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private float _startSpeed = 1f;

        [Space(10)]
        [Header("Dependencies")]
        [SerializeField] private InputManager _InputManager;
        [SerializeField] private PlayerManager _PlayerManager;
        [SerializeField] private ObstacleManager _ObstacleManager;
        [SerializeField] private UIManager _UIManager;
        [SerializeField] private RoadManager _RoadManager;

        private IInputManager _inputManager => _InputManager;
        private IPlayerManager _playerManager => _PlayerManager;
        private IObstacleManager _obstacleManager => _ObstacleManager;
        private IUIManager _uIManager => _UIManager;
        private IRoadManager _roadManager => _RoadManager;

        private void Awake()
        {
            Application.targetFrameRate = 60;

            _inputManager.Init();
            _playerManager.Init();
            _obstacleManager.Init();
            _roadManager.Init();

            _obstacleManager.GameOver += GameOver;

            _uIManager.ShowWindow(WindowsTypes.StartWindow);

            StartWindow startWindow = _uIManager.GetStartWidnow;

            startWindow.GameStarted += StartGame;
        }

        private void StartGame()
        {
            _obstacleManager.StartSpawn();
            _playerManager.StartMove();
            _roadManager.StartRoadSimulation(10f);
            _uIManager.ShowWindow(WindowsTypes.InGameWindow);
        }

        private void GameOver()
        {
            _playerManager.DestroyPlayer();
            _uIManager.ShowWindow(WindowsTypes.GameOverWidnow);
            _roadManager.StopRoad();

            GameOverWindow gameOverWindow = _uIManager.GetGameOverWindow;

            gameOverWindow.RestartLevel += Reload;
        }

        private void Reload()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
