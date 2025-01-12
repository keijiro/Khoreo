using UnityEngine;
using UnityEngine.Events;
using Lasp;

namespace Khoreo {

public sealed class AudioThreshold : MonoBehaviour
{
    #region Scene object references

    [SerializeField] AudioLevelTracker _tracker = null;
    [SerializeField] UnityEvent _onEvent = null;
    [SerializeField] UnityEvent _offEvent = null;

    #endregion

    #region Public properties

    [field:SerializeField] public float Threshold { get; set; } = 0;
    [field:SerializeField] public float Delay { get; set; } = 0.4f;

    #endregion

    #region Private members

    bool _flag;
    float _timer;

    #endregion

    #region MonoBehaviour implementation

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
            if (_tracker.normalizedLevel > Threshold)
            {
                _onEvent.Invoke();
                _timer = Delay;
                _flag = true;
            }
        }
        else
        {
            // Currently on: Turn off when the timer ends.
            if (_tracker.normalizedLevel > Threshold)
            {
                _timer = Delay;
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

    #endregion
}

} // namespace Khoreo
