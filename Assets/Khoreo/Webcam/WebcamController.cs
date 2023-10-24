using UnityEngine;
using UnityEngine.VFX;

namespace Khoreo
{
    public sealed class WebcamController : MonoBehaviour
    {
        [SerializeField] string _deviceName = null;
        [SerializeField, Range(0, 1)] float _threshold = 0.1f;
        [SerializeField, Range(0, 1)] float _contrast = 0.5f;

        [SerializeField, HideInInspector] Shader _filterShader = null;

        WebCamTexture _webcam;
        Material _material;
        RenderTexture _filtered;

        void Start()
        {
            _webcam = new WebCamTexture(_deviceName, 1280, 720, 60);
            _material = new Material(_filterShader);
            _filtered = new RenderTexture(1024, 1024, 0);
            _filtered.useMipMap = true;

            _webcam.Play();

            GetComponent<VisualEffect>()
              .SetTexture("WebcamTexture", _filtered);
        }

        void OnDestroy()
        {
            _webcam.Stop();

            Destroy(_webcam);
            Destroy(_material);
            Destroy(_filtered);
        }

        void Update()
        {
            _material.SetVector
              ("_FilterParams", new Vector2(_threshold, _contrast));

            Graphics.Blit(_webcam, _filtered, _material, 0);
        }
    }
}
