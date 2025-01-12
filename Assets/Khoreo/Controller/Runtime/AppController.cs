using UnityEngine;

namespace Khoreo {

public sealed class AppController : MonoBehaviour
{
#if !UNITY_EDITOR
    void Start()
      => Cursor.visible = false;
#endif
}

} // namespace Khoreo
