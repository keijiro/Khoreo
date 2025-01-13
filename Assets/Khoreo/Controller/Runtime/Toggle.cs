using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Khoreo {

public sealed class Toggle : MonoBehaviour
{
    #region Editable attributes

    [SerializeField] InputAction _action = null;
    [SerializeField] UnityEvent _offEvent = null;
    [SerializeField] UnityEvent _onEvent = null;

    #endregion

    #region Public properties and methods

    [field:SerializeField] public bool IsOn { get; private set; }

    public void Flip()
    {
        IsOn = !IsOn;
        InvokeEvent();
    }

    #endregion

    #region Private members

    void OnPerformed(InputAction.CallbackContext ctx)
      => Flip();

    void InvokeEvent()
      => (IsOn ? _onEvent : _offEvent).Invoke();

    #endregion

    #region MonoBehaviour implementation

    void OnEnable()
    {
        _action.performed += OnPerformed;
        _action.Enable();
    }

    void OnDisable()
    {
        _action.performed -= OnPerformed;
        _action.Disable();
    }

    void Start()
      => InvokeEvent();

    #endregion
}

} // namespace Khoreo
