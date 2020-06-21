using UnityEngine;

namespace Khoreo
{
    class LightQuad : MonoBehaviour
    {
        Light _light;
        Renderer _renderer;

        void Start()
        {
            _light = transform.parent.GetComponent<Light>();
            _renderer = GetComponent<Renderer>();
        }

        void LateUpdate()
        {
            var color = _light.color.linear * _light.intensity;
            _renderer.material.SetColor("_EmissiveColor", color);
        }
    }
}
