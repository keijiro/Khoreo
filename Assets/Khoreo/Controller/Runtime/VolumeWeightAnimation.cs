using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

namespace Khoreo
{
    public sealed class VolumeWeightAnimation : MonoBehaviour
    {
        #region Editable attributes

        [SerializeField] AnimationCurve _curve = DefaultCurve;
        [SerializeField] float _speed = 1;
        [SerializeField] Volume _volume = null;
        [SerializeField] InputAction _trigger = null;

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
          => _volume.weight = _curve.Evaluate(_time);

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
