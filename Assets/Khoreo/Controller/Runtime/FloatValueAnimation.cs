using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Khoreo
{
    public sealed class FloatValueAnimation : MonoBehaviour
    {
        #region Local class

        [System.Serializable] class FloatEvent : UnityEvent<float> {}

        #endregion

        #region Editable attributes

        [SerializeField] AnimationCurve _curve = DefaultCurve;
        [SerializeField] float _speed = 1;
        [SerializeField] InputAction _trigger = null;
        [SerializeField] FloatEvent _event = null;

        #endregion

        #region Public members

        public float Speed
          { get => _speed; set => _speed = value; }

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

        static AnimationCurve DefaultCurve
          { get => AnimationCurve.Linear(0, 1, 1, 0); }

        float _time = 1e+5f;

        bool IsActive
          => enabled && gameObject.activeInHierarchy;

        void ApplyCurve()
          => _event.Invoke(_curve.Evaluate(_time));

        void OnPerformed(InputAction.CallbackContext ctx)
          => Play();

        #endregion

        #region MonoBehaviour

        void OnEnable()
        {
            _trigger.performed += OnPerformed;
            _trigger.Enable();
        }

        void OnDisable()
        {
            Stop();
            _trigger.Disable();
            _trigger.performed -= OnPerformed;
        }

        void Start()
          => Stop();

        void Update()
        {
            ApplyCurve();
            _time += Time.deltaTime * _speed;
        }

        #endregion
    }
}
