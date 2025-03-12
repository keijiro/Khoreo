using UnityEngine;
using UnityEngine.Events;
using Lasp;

namespace Khoreo {

public sealed class AudioThreshold : MonoBehaviour
{
    #region Public properties

    [field:SerializeField] public AudioLevelTracker Tracker { get; set; }
    [field:SerializeField] public UnityEvent OnEvent { get; set; }
    [field:SerializeField] public UnityEvent OffEvent { get; set; }
    [field:SerializeField] public float Threshold { get; set; }
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
            OffEvent.Invoke();
        }
    }

    void Start()
      => OffEvent.Invoke(); // Starts from the off state.

    void Update()
    {
        if (!_flag)
        {
            // Currently off: Turn on when the input hits the threshold.
            if (Tracker.normalizedLevel > Threshold)
            {
                OnEvent.Invoke();
                _timer = Delay;
                _flag = true;
            }
        }
        else
        {
            // Currently on: Turn off when the timer ends.
            if (Tracker.normalizedLevel > Threshold)
            {
                _timer = Delay;
            }
            else
            {
                _timer -= Time.deltaTime;
                if (_timer <= 0)
                {
                    OffEvent.Invoke();
                    _flag = false;
                }
            }
        }
    }

    #endregion
}

} // namespace Khoreo
