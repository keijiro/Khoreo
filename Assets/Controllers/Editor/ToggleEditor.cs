using UnityEditor;
using KlutterTools.InspectorUtils;

namespace Khoreo {

[CustomEditor(typeof(Toggle)), CanEditMultipleObjects]
sealed class ToggleEditor : Editor
{
    AutoProperty _isOn = null;
    AutoProperty _action = null;
    AutoProperty _offEvent = null;
    AutoProperty _onEvent = null;

    void OnEnable()
      => AutoProperty.Scan(this);

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(_isOn);
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(_action);
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(_offEvent);
        EditorGUILayout.PropertyField(_onEvent);
        serializedObject.ApplyModifiedProperties();
    }
}

} // namespace Khoreo
