using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

namespace Khoreo {

public sealed class VolumeWeightAnimation : MonoBehaviour
{
    #region Editable attributes

    [SerializeField] AnimationCurve _curve = SimpleCurve.RampDown();
    [SerializeField] InputAction _trigger = null;
    [SerializeField] Volume _volume = null;

    #endregion

    #region Public properties

    [field:SerializeField] float Speed { get; set; } = 1;

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
      => _volume.weight = _curve.Evaluate(_time);

    #endregion

    #region MonoBehaviour

    void OnEnable()
      => _trigger.Enable();

    void OnDisable()
    {
        Stop();
        _trigger.Disable();
    }

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
