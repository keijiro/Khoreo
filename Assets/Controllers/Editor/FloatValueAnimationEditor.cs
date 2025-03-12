using UnityEditor;
using KlutterTools.InspectorUtils;

namespace Khoreo {

[CustomEditor(typeof(FloatValueAnimation)), CanEditMultipleObjects]
sealed class FloatValueAnimationEditor : Editor
{
    AutoProperty _curve = null;
    AutoProperty _trigger = null;
    AutoProperty _event = null;
    AutoProperty _speed = null;

    void OnEnable()
      => AutoProperty.Scan(this);

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(_curve);
        EditorGUILayout.PropertyField(_speed);
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(_trigger);
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(_event);
        serializedObject.ApplyModifiedProperties();
    }
}

} // namespace Khoreo
