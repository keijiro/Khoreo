using UnityEngine;

namespace Khoreo {

public static class SimpleCurve
{
    public static AnimationCurve RampDown()
      => AnimationCurve.Linear(0, 1, 1, 0);
}

} // namespace Khoreo
