using UnityEditor;
using KlutterTools.InspectorUtils;

namespace Khoreo {

[CustomEditor(typeof(Trigger)), CanEditMultipleObjects]
sealed class TriggerEditor : Editor
{
    AutoProperty _action = null;
    AutoProperty _event = null;

    void OnEnable()
      => AutoProperty.Scan(this);

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(_action);
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(_event);
        serializedObject.ApplyModifiedProperties();
    }
}

} // namespace Khoreo
