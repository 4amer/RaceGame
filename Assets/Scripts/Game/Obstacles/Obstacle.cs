using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Game.Obstacle
{
    public class Obstacle : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rb;

        public event Action<Obstacle> OnDie;
        public event Action CollidedWithPlayer;

        private Timer _timerToDie;
        public void Init(float liveTime)
        {
            _timerToDie = new Timer();

            _timerToDie.duration = liveTime;

            _timerToDie.OnTimerStop += Die;

            _timerToDie.Start();
        }

        public void PositionChanged(float speed)
        {
            _rb.MovePosition(new Vector3(0, 0, transform.position.z + speed * (-1)));
        }

        private void Die()
        {
            _timerToDie.OnTimerStop -= Die;
            OnDie?.Invoke(this);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                CollidedWithPlayer?.Invoke();
            }
        }

        private void OnDestroy()
        {
            if (_timerToDie != null)
            {
                _timerToDie.Stop();
            }
        }
    }
}