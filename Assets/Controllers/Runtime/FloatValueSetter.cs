using UnityEngine;
using Klak.Math;

namespace Khoreo {

public sealed class FloatValueSetter : MonoBehaviour
{
    #region Editable attributes

    [SerializeField] FloatValueEvent _event = null;

    #endregion

    #region Public properties

    [field:SerializeField] public float Speed { get; set; } = 10;
    [field:SerializeField] public float TargetValue { get; set; }

    public float CurrentValue
      { get => _currentValueBacking;
        set => _event.Invoke(_currentValueBacking = value); }

    #endregion

    #region Private members

    float _currentValueBacking;
    float _velocity;

    #endregion

    #region MonoBehaviour implementation

    void Start()
      => CurrentValue = TargetValue;

    void Update()
      => (CurrentValue, _velocity) =
            CdsTween.Step((CurrentValue, _velocity), TargetValue, Speed);

    #endregion
}

} // namespace Khoreo
