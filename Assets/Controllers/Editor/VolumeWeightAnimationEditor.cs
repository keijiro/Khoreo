using UnityEditor;
using KlutterTools.InspectorUtils;

namespace Khoreo {

[CustomEditor(typeof(VolumeWeightAnimation)), CanEditMultipleObjects]
sealed class VolumeWeightAnimationEditor : Editor
{
    AutoProperty _curve = null;
    AutoProperty _speed = null;
    AutoProperty _trigger = null;
    AutoProperty _volume = null;

    void OnEnable()
      => AutoProperty.Scan(this);

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(_curve);
        EditorGUILayout.PropertyField(_speed);
        EditorGUILayout.PropertyField(_volume);
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(_trigger);
        serializedObject.ApplyModifiedProperties();
    }
}

} // namespace Khoreo
