using UnityEditor;
using KlutterTools.InspectorUtils;

namespace Khoreo {

[CustomEditor(typeof(FloatValueSetter)), CanEditMultipleObjects]
sealed class FloatValueSetterEditor : Editor
{
    AutoProperty _speed = null;
    AutoProperty _targetValue = null;
    AutoProperty _event = null;

    void OnEnable()
      => AutoProperty.Scan(this);

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(_speed);
        EditorGUILayout.PropertyField(_targetValue);
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(_event);
        serializedObject.ApplyModifiedProperties();
    }
}

} // namespace Khoreo
