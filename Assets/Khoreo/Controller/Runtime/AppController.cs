using UnityEngine;

namespace Khoreo
{
    sealed class AppController : MonoBehaviour
    {
        void Start()
        {
            Application.targetFrameRate = 60;
#if !UNITY_EDITOR
            Cursor.visible = false;
#endif
        }
    }
}
