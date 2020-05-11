using UnityEngine;
using UnityEngine.VFX;

namespace Khoreo
{
    public sealed class Webcam : MonoBehaviour
    {
        [SerializeField] VisualEffect _vfx = null;

        WebCamTexture _webcam;

        void Start()
        {
            _webcam = new WebCamTexture();
            _webcam.Play();
            _vfx.SetTexture("WebcamTexture", _webcam);
        }
    }
}
