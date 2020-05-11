using UnityEngine;

namespace Khoreo
{
    public sealed class LookAt : MonoBehaviour
    {
        [SerializeField] Transform _target = null;

        Vector3 Flatten(Vector3 v)
          => Vector3.Scale(v, new Vector3(1, 0, 1)).normalized;

        void Update()
          => transform.rotation = Quaternion.LookRotation
               (Flatten(_target.position - transform.position));
    }
}
