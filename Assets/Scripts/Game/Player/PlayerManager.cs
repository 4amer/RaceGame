using Game.Input.Manager;
using UnityEngine;

namespace Game.Player.Manager
{
    public class PlayerManager : MonoBehaviour, IPlayerManager
    {
        [SerializeField] private InputManager _InputManager = null;

        [SerializeField] private float _moveMultiplayer = 1f;

        [Space(10)]
        [Header("Player")]
        [SerializeField] private GameObject _player = null;
        [SerializeField] private GameObject _playerGrapfics = null;
        [SerializeField] private Rigidbody _playerGB = null;
        [SerializeField] private ParticleSystem _exploadeParticles = null;

        private IInputManager _inputManager => _InputManager;

        private float _xMovement = 0;
        public void Init()
        {
            
        }

        public void StartMove()
        {
            _inputManager.XValueChanged += MovePlayer;
        }

        private void FixedUpdate()
        {
            Vector3 _playerPosition = _player.transform.position;
            _playerGB.MovePosition(new Vector3(Mathf.Clamp((_playerPosition.x + _xMovement * _moveMultiplayer * Time.fixedDeltaTime), -4.5f, -1.5f), _playerPosition.y, _playerPosition.z));
        }

        private void MovePlayer(float direction)
        {
            _xMovement = direction;
        }

        public void DestroyPlayer()
        {
            _inputManager.XValueChanged -= MovePlayer;
            _exploadeParticles.Play();
            _playerGrapfics.SetActive(false);
        }

        private void OnDestroy()
        {
            _inputManager.XValueChanged -= MovePlayer;
        }
    }

    public interface IPlayerManager
    {
        public void Init();
        public void StartMove();
        public void DestroyPlayer();
    }
}