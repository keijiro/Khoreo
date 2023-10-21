using UnityEngine;
using UnityEngine.Events;
using Klak.Math;

namespace Khoreo
{
    public sealed class FloatValueRelay : MonoBehaviour
    {
        #region Local class

        [System.Serializable] class FloatEvent : UnityEvent<float> {}

        #endregion

        #region Editable attributes

        [SerializeField] float _speed = 10;
        [SerializeField] float _targetValue = 0;
        [SerializeField] FloatEvent _event = null;

        #endregion

        #region Public members

        public float Speed
          { get => _speed; set => _speed = value; }

        public float targetValue
          { get => _targetValue; set => _targetValue = value; }

        public float currentValue
          { get => _currentValue; set => _currentValue = value; }

        #endregion

        #region MonoBehaviour

        float _currentValue;
        float _velocity;

        void Start()
        {
            _currentValue = _targetValue;

            _event.Invoke(_currentValue);
        }

        void Update()
        {
            (_currentValue, _velocity) = CdsTween.Step
              ((_currentValue, _velocity), _targetValue, _speed);

            _event.Invoke(_currentValue);
        }

        #endregion
    }
}
