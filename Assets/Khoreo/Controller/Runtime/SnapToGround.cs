using UnityEngine;

namespace Khoreo
{
    public sealed class SnapToGround : MonoBehaviour
    {
        [SerializeField] Transform _target = null;
        [SerializeField] float _offset = 0;

        void LateUpdate()
        {
            var p = _target.position;
            p.y = _offset;
            transform.position = p;
        }
    }
}
