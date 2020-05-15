using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.VFX.Utility;

namespace Khoreo
{
    [AddComponentMenu("VFX/Property Binders/Khoreo/Float Value Relay Binder")]
    [VFXBinder("Khoreo/Float Value Relay")]
    sealed class VFXFloatValueRelayBinder : VFXBinderBase
    {
        public string Property
          { get => (string)_property; set => _property = value; }

        [VFXPropertyBinding("System.Single"), SerializeField]
        ExposedProperty _property = "FloatValue";

        public FloatValueRelay Target = null;

        public override bool IsValid(VisualEffect component)
          => Target != null && component.HasFloat(_property);

        public override void UpdateBinding(VisualEffect component)
          => component.SetFloat(_property, Target.currentValue);

        public override string ToString()
          => $"Float Value : '{_property}' -> {Target?.name ?? "(null)"}";
    }
}
