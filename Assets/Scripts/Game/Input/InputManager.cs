using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Input.Manager
{
    public class InputManager : MonoBehaviour, IInputManager
    {
        [SerializeField] private InputActionReference _swipeReference = default;

        private float _xSwipe = 0f;

        public Action<float> XValueChanged { get; set; }

        public void Init()
        {
            _swipeReference.action.performed += ctx => CheckSwipe(ctx);
        }

        private void CheckSwipe(InputAction.CallbackContext ctx)
        {
            Vector2 delta = ctx.ReadValue<Vector2>();
            _xSwipe = delta.x;
            XValueChanged?.Invoke(_xSwipe);
        }

        private void OnDestroy()
        {
            _swipeReference.action.performed -= ctx => CheckSwipe(ctx);
        }
    }

    public interface IInputManager
    {
        public Action<float> XValueChanged { get; set; }
        public void Init();
    }
}