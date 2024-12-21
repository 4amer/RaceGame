using System.Collections.Generic;
using Game.Obstacle;
using UnityEngine;

namespace Game.Road.Manager
{
    //Пишу это уже в час ночи, устал, поэтому скапироввл всю логику с ObstacleManager
    public class RoadManager : MonoBehaviour, IRoadManager
    {
        [SerializeField] private GameObject _roadPrefab;
        [SerializeField] private float _spawnDelay = 5f;
        [SerializeField] private GameObject _roadSpawnPoint;

        [SerializeField] private Road[] _alreadySPawnedRoad = new Road[0];

        private Queue<Road> _spawnedRoad = new Queue<Road>();

        private Timer _timerToSpawn;

        private float _movementSpeed = 0;

        private float _previousY = 0.01f;

        //private float _liveTime = 2.1f;

        private event System.Action<float> UpdatePosition;

        public void Init()
        {
            _timerToSpawn = new Timer();
        }

        private void FixedUpdate()
        {
            UpdatePosition?.Invoke(_movementSpeed * Time.deltaTime);
        }

        public void StartRoadSimulation(float speed)
        {
            _movementSpeed = speed;

            _timerToSpawn.timeStep = _spawnDelay;
            _timerToSpawn.OnTimerUpdated += SpawnRoad;
            _timerToSpawn.StartInfinity();

            SubscribeAllSpawnedRoads();
        }

        private void SubscribeAllSpawnedRoads()
        {
            foreach (Road road in _alreadySPawnedRoad)
            {
                //road.OnDie += OnDestroyRoad;

                //road.Init(_liveTime);

                UpdatePosition += road.PositionChanged;

                _spawnedRoad.Enqueue(road);
            }
        }

        private void SpawnRoad()
        {
            Road roadInstance = _spawnedRoad.Dequeue();

            if (_previousY == 0.01f)
            {
                _previousY = -0.01f;
            }
            else
            {
                _previousY = 0.01f;
            }

            roadInstance.transform.localPosition = new Vector3(0, _previousY, 0);

            _spawnedRoad.Enqueue(roadInstance);

            //road.OnDie += OnDestroyRoad;

            //road.Init(_liveTime);

            //UpdatePosition += road.PositionChanged;

            //_spawnedRoad.Add(road);
        }

        public void StopRoad()
        {
            /*foreach (Road obstacle in _spawnedRoad)
            {
                obstacle.OnDie -= OnDestroyRoad;
            }*/

            _timerToSpawn.Stop();
            UpdatePosition = null;
        }

        /*private void OnDestroyRoad(Road road)
        {
            //_spawnedRoad.Remove(road);
            //road.Init(_liveTime);
            road.gameObject.transform.localPosition = Vector3.zero;

            //road.OnDie -= OnDestroyRoad;
            //UpdatePosition -= road.PositionChanged;
            //Destroy(road.gameObject);
        }*/
    }

    public interface IRoadManager
    {
        public void Init();
        public void StartRoadSimulation(float speed);
        public void StopRoad();
    }
}
