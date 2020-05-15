using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Khoreo
{
    public sealed class Toggle : MonoBehaviour
    {
        [SerializeField] bool _state = false;
        [SerializeField] InputAction _action = null;
        [SerializeField] UnityEvent _offEvent = null;
        [SerializeField] UnityEvent _onEvent = null;

        public bool IsOn => _state;

        public void Flip()
        {
            _state = !_state;
            (_state ? _onEvent : _offEvent).Invoke();
        }

        void OnEnable()
        {
            _action.performed += OnPerformed;
            _action.Enable();
        }

        void OnDisable()
        {
            _action.Disable();
            _action.performed -= OnPerformed;
        }

        void OnPerformed(InputAction.CallbackContext ctx)
          => Flip();

        void Start()
          => (_state ? _onEvent : _offEvent).Invoke();
    }
}
