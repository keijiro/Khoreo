using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.VFX.Utility;

namespace Khoreo {

[AddComponentMenu("VFX/Property Binders/Khoreo/Toggle Binder")]
[VFXBinder("Khoreo/Toggle")]
sealed class VFXToggleBinder : VFXBinderBase
{
    [VFXPropertyBinding("System.Boolean"), SerializeField]
    public ExposedProperty Property = "BoolValue";

    public Toggle Target = null;

    public override bool IsValid(VisualEffect component)
      => Target != null && component.HasBool(Property);

    public override void UpdateBinding(VisualEffect component)
      => component.SetBool(Property, Target.IsOn);

    public override string ToString()
      => $"Toggle : '{Property}' -> {Target?.name ?? "(null)"}";
}

} // namespace Khoreo
