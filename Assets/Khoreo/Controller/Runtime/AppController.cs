using UnityEngine;

namespace Khoreo
{
    sealed class AppController : MonoBehaviour
    {
        void Start()
        {
#if !UNITY_EDITOR
            Cursor.visible = false;
#endif
        }
    }
}
