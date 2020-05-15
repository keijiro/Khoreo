using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Khoreo
{
    public sealed class Trigger : MonoBehaviour
    {
        [SerializeField] InputAction _action = null;
        [SerializeField] UnityEvent _event = null;

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
          => _event.Invoke();
    }
}
