using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Khoreo {

public sealed class Trigger : MonoBehaviour
{
    #region Editable attributes

    [SerializeField] InputAction _action = null;
    [SerializeField] UnityEvent _event = null;

    #endregion

    #region Private members

    void OnPerformed(InputAction.CallbackContext ctx)
      => _event.Invoke();

    #endregion

    #region MonoBehaviour implementation

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

    #endregion
}

} // namespace Khoreo
