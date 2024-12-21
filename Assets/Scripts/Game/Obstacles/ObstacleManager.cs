using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

namespace Game.Obstacle.Manager
{
    public class ObstacleManager : MonoBehaviour, IObstacleManager
    {
        [SerializeField] private GameObject[] _spawnPositions = new GameObject[0];

        [SerializeField] private GameObject[] _obstaclesPrefabs = new GameObject[0];

        [SerializeField] private float _nextDifficultyDeley = 20f;

        [SerializeField] private float _obstacleLiveTime = 10f;

        private List<Obstacle> _spawnedObstacles = new List<Obstacle>();

        private float _obstacleSpeed = 10f;

        private int _previousSpawnPointIndex;
        private int _previousObstacleIndex;

        private float deleyToSpawn = 1f;

        public System.Action GameOver { get; set; }
        private event System.Action<float> UpdatePosition;

        private Timer _spawnTimer = new Timer();
        private Timer _difficaltyTimer = new Timer();
        public void Init()
        {
            _spawnTimer = new Timer();
            _spawnTimer.timeStep = deleyToSpawn;
            _spawnTimer.OnTimerUpdated += SpawnObstacles;

            _difficaltyTimer.OnTimerUpdated += SubstractSpawnDeley;
        }

        private void FixedUpdate()
        {
            UpdatePosition?.Invoke(_obstacleSpeed * Time.deltaTime);
        }

        public void StartSpawn()
        {
            deleyToSpawn = 1f;
            _difficaltyTimer.timeStep = _nextDifficultyDeley;
            _difficaltyTimer.StartInfinity();

            _spawnTimer.StartInfinity();
        }

        private void SubstractSpawnDeley()
        {
            deleyToSpawn -= 0.01f;
            if (deleyToSpawn < 0.15f) deleyToSpawn = 0.15f;
            Debug.Log(deleyToSpawn);
            _spawnTimer.timeStep = deleyToSpawn;
            Debug.Log("Difficulty dicrised");
        }

        private void SpawnObstacles()
        {
            int spawnPointIndex = -1;
            int obstacleIndex = -1;

            do
            {
                obstacleIndex = Random.Range(0, _obstaclesPrefabs.Length);
            } while (_previousObstacleIndex == obstacleIndex);

            _previousObstacleIndex = obstacleIndex;

            do
            {
                spawnPointIndex = Random.Range(0, _spawnPositions.Length);
            } while (_previousSpawnPointIndex == spawnPointIndex);

            _previousSpawnPointIndex = spawnPointIndex;

            GameObject carInstance = Instantiate(_obstaclesPrefabs[obstacleIndex], Vector3.zero, Quaternion.identity, _spawnPositions[spawnPointIndex].transform);

            carInstance.transform.localPosition = Vector3.zero;

            Obstacle obstacle = carInstance.GetComponent<Obstacle>();

            obstacle.OnDie += OnDestroyObstacle;

            obstacle.CollidedWithPlayer += ObstacleCollidedWithPlayer;

            obstacle.Init(_obstacleLiveTime);

            UpdatePosition += obstacle.PositionChanged;

            _spawnedObstacles.Add(obstacle);
        }

        private void ObstacleCollidedWithPlayer()
        {
            foreach(Obstacle obstacle in _spawnedObstacles)
            {
                obstacle.OnDie -= OnDestroyObstacle;
                obstacle.CollidedWithPlayer -= ObstacleCollidedWithPlayer;
            }

            _spawnTimer.Stop();
            _difficaltyTimer.Stop();

            UpdatePosition = null;
            GameOver?.Invoke();
        }

        private void OnDestroyObstacle(Obstacle obstacle)
        {
            _spawnedObstacles.Remove(obstacle);

            obstacle.OnDie -= OnDestroyObstacle;
            UpdatePosition -= obstacle.PositionChanged;
            obstacle.CollidedWithPlayer -= ObstacleCollidedWithPlayer;
            Destroy(obstacle.gameObject);
        }

        public void EndSpawn()
        {
            _spawnTimer.Stop();
            _difficaltyTimer.Stop();
        }

        private void OnDestroy()
        {
            EndSpawn();
        }
    }

    public interface IObstacleManager
    {
        public System.Action GameOver { get; set; }
        public void Init();
        public void StartSpawn();
        public void EndSpawn(); 
    }
}