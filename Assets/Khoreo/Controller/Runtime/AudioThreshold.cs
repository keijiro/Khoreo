using UnityEngine;
using UnityEngine.Events;
using Lasp;

namespace Khoreo
{
    public sealed class AudioThreshold : MonoBehaviour
    {
        [SerializeField] AudioLevelTracker _tracker = null;
        [SerializeField] float _threshold = 0;
        [SerializeField] float _delay = 0.2f;
        [SerializeField] UnityEvent _onEvent = null;
        [SerializeField] UnityEvent _offEvent = null;

        bool _flag;
        float _timer;

        void OnDisable()
        {
            // Force off on disable
            if (_flag)
            {
                _flag = false;
                _offEvent.Invoke();
            }
        }

        void Start()
          => _offEvent.Invoke(); // Starts from the off state.

        void Update()
        {
            if (!_flag)
            {
                // Currently off: Turn on when the input hits the threshold.
                if (_tracker.normalizedLevel > _threshold)
                {
                    _onEvent.Invoke();
                    _timer = _delay;
                    _flag = true;
                }
            }
            else
            {
                // Currently on: Turn off when the timer ends.
                if (_tracker.normalizedLevel > _threshold)
                {
                    _timer = _delay;
                }
                else
                {
                    _timer -= Time.deltaTime;
                    if (_timer <= 0)
                    {
                        _offEvent.Invoke();
                        _flag = false;
                    }
                }
            }
        }
    }
}
