using UnityEngine;
using Unity.Mathematics;
using Klak.Math;

namespace Khoreo
{
    public sealed class WebCamManager : MonoBehaviour
    {
        [SerializeField] Camera _mainCamera = null;
        [SerializeField] Transform _dancerHead = null;
        [SerializeField] RectTransform _uiRoot = null;

        CdsTween2 _uiPosition = new CdsTween2(0, 3);
        WebCamTexture _webCam;

        void Start()
        {
            _webCam = new WebCamTexture();
            _webCam.Play();

            var raw = _uiRoot.GetComponent<UnityEngine.UI.RawImage>();
            raw.texture = _webCam;
        }

        void Update()
        {
            var parent = (RectTransform)_uiRoot.parent;

            var prj = _mainCamera.WorldToScreenPoint(_dancerHead.position);

            var head = ((float3)prj).xy;
            var size = (float2)parent.rect.size * parent.localScale.x;
            var center = size / 2;

            var p = center - math.normalize(head - center) * size / 3;

            _uiPosition.Step(p);

            _uiRoot.position = math.float3(_uiPosition.Current, 0);
        }
    }
}
