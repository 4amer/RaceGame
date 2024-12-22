using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Input.Manager
{
    public class InputManager : MonoBehaviour, IInputManager
    {
        [SerializeField] private InputActionReference _swipeReference = default;
        [SerializeField] private InputActionReference _clickReference = default;

        private float _xSwipe = 0f;

        private bool _isLeftButtonClicked = false;

        public Action<float> XValueChanged { get; set; }

        public void Init()
        {
            _swipeReference.action.performed += ctx => CheckSwipe(ctx);
            _clickReference.action.started += ctx => ButtonPressed(ctx);
            _clickReference.action.canceled += ctx => ButtonRelised(ctx);
        }

        private void CheckSwipe(InputAction.CallbackContext ctx)
        {
            if (_isLeftButtonClicked)
            {
                Vector2 delta = ctx.ReadValue<Vector2>();
                _xSwipe = delta.x;
                XValueChanged?.Invoke(_xSwipe);
            }
        }

        private void ButtonPressed(InputAction.CallbackContext ctx)
        {
            _isLeftButtonClicked = true;
        }

        private void ButtonRelised(InputAction.CallbackContext ctx)
        {
            _isLeftButtonClicked = false;
        }

        private void OnDestroy()
        {
            _swipeReference.action.performed -= ctx => CheckSwipe(ctx);
            _clickReference.action.started -= ctx => ButtonPressed(ctx);
            _clickReference.action.canceled -= ctx => ButtonRelised(ctx);
        }
    }

    public interface IInputManager
    {
        public Action<float> XValueChanged { get; set; }
        public void Init();
    }
}