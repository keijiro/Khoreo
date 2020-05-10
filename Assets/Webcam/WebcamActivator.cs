using UnityEngine;

namespace Khoreo
{
    public sealed class WebcamActivator : MonoBehaviour
    {
        WebCamTexture _webcam;

        void Start()
        {
            _webcam = new WebCamTexture();
            _webcam.Play();

            var prop = new MaterialPropertyBlock();
            prop.SetTexture("WebcamTexture", _webcam);

            GetComponent<Renderer>().SetPropertyBlock(prop);
        }
    }
}
