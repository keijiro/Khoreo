using UnityEditor;
using KlutterTools.InspectorUtils;

namespace Khoreo {

[CustomEditor(typeof(AudioThreshold)), CanEditMultipleObjects]
sealed class AudioThresholdEditor : Editor
{
    AutoProperty _tracker = null;
    AutoProperty _onEvent = null;
    AutoProperty _offEvent = null;
    AutoProperty _threshold = null;
    AutoProperty _delay = null;

    void OnEnable()
      => AutoProperty.Scan(this);

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(_tracker);
        EditorGUILayout.PropertyField(_threshold);
        EditorGUILayout.PropertyField(_delay);
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(_onEvent);
        EditorGUILayout.PropertyField(_offEvent);
        serializedObject.ApplyModifiedProperties();
    }
}

} // namespace Khoreo
