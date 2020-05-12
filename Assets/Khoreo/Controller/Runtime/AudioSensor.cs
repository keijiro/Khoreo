using UnityEngine;
using Lasp;

namespace Khoreo
{
    public sealed class AudioSensor : MonoBehaviour
    {
        [SerializeField] AudioLevelTracker _tracker = null;
        [SerializeField] float _threshold = 0;
        [SerializeField] float _delay = 0.2f;
        [SerializeField] ObjectSwitcher _target = null;

        float _timer;

        void OnValidate()
          => _target.Owner = transform;

        void Update()
        {
            if (_tracker.normalizedLevel > _threshold)
                _timer = _delay;
            else
                _timer -= Time.deltaTime;

            _target.Active = _timer >= 0;
        }
    }
}
