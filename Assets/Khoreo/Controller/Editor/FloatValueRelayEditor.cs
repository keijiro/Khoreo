using UnityEditor;

namespace Khoreo
{
    [CustomEditor(typeof(FloatValueRelay)), CanEditMultipleObjects]
    sealed class FloatValueRelayEditor : Editor
    {
        SerializedProperty _speed;
        SerializedProperty _targetValue;
        SerializedProperty _event;

        void OnEnable()
        {
            _speed       = serializedObject.FindProperty("_speed");
            _targetValue = serializedObject.FindProperty("_targetValue");
            _event       = serializedObject.FindProperty("_event");
        }

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
}
