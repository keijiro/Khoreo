using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.VFX.Utility;

namespace Khoreo
{
    [AddComponentMenu("VFX/Property Binders/Khoreo/Toggle Binder")]
    [VFXBinder("Khoreo/Toggle")]
    sealed class VFXToggleBinder : VFXBinderBase
    {
        public string Property
          { get => (string)_property; set => _property = value; }

        [VFXPropertyBinding("System.Boolean"), SerializeField]
        ExposedProperty _property = "BoolValue";

        public Toggle Target = null;

        public override bool IsValid(VisualEffect component)
          => Target != null && component.HasBool(_property);

        public override void UpdateBinding(VisualEffect component)
          => component.SetBool(_property, Target.IsOn);

        public override string ToString()
          => $"Toggle : '{_property}' -> {Target?.name ?? "(null)"}";
    }
}
