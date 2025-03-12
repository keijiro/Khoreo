using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

namespace Khoreo {

public sealed class VolumeWeightRandomizer : MonoBehaviour
{
    #region Editable attributes

    [SerializeField] Volume _volume = null;
    [SerializeField] InputAction _trigger = null;
    [SerializeField] InputAction _reset = null;

    #endregion

    #region Public methods

    public void Randomize()
    {
        if (!IsActive) return;
        _volume.weight = Random.value;
    }

    public void SetWeight(float weight)
    {
        if (!IsActive) return;
        _volume.weight = weight;
    }

    #endregion

    #region Private members

    bool IsActive
      => enabled && gameObject.activeInHierarchy;

    void OnTriggerPerformed(InputAction.CallbackContext ctx)
      => Randomize();

    void OnResetPerformed(InputAction.CallbackContext ctx)
      => SetWeight(0);

    #endregion

    #region MonoBehaviour

    void OnEnable()
    {
        _trigger.performed += OnTriggerPerformed;
        _reset.performed += OnResetPerformed;
        _trigger.Enable();
        _reset.Enable();
    }

    void OnDisable()
    {
        _trigger.performed -= OnTriggerPerformed;
        _reset.performed -= OnResetPerformed;
        _trigger.Disable();
        _reset.Disable();
    }

    #endregion
}

} // namespace Khoreo
