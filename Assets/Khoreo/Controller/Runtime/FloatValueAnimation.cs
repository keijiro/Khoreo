using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Khoreo {

[System.Serializable]
public sealed class FloatValueEvent : UnityEvent<float> {}

public sealed class FloatValueAnimation : MonoBehaviour
{
    #region Editable attributes

    [SerializeField] AnimationCurve _curve = SimpleCurve.RampDown();
    [SerializeField] InputAction _trigger = null;
    [SerializeField] FloatValueEvent _event = null;

    #endregion

    #region Public properties

    [field:SerializeField] public float Speed { get; set; } = 1;

    #endregion

    #region Public methods

    public void Play()
    {
        if (!IsActive) return;
        _time = 0;
        ApplyCurve();
    }

    public void Stop()
    {
        _time = 1e+5f;
        ApplyCurve();
    }

    #endregion

    #region Private members

    float _time = 1e+5f;

    bool IsActive
      => enabled && gameObject.activeInHierarchy;

    void ApplyCurve()
      => _event.Invoke(_curve.Evaluate(_time));

    #endregion

    #region MonoBehaviour implementation

    void OnEnable()
      => _trigger.Enable();

    void OnDisable()
      => _trigger.Disable();

    void Start()
      => Stop();

    void Update()
    {
        if (_trigger.triggered)
        {
            Play();
        }
        else
        {
            _time += Time.deltaTime * Speed;
            ApplyCurve();
        }
    }

    #endregion
}

} // namespace Khoreo
