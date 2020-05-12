using UnityEditor;

namespace Khoreo
{
    [CustomEditor(typeof(FloatValueAnimation)), CanEditMultipleObjects]
    sealed class FloatValueAnimationEditor : Editor
    {
        SerializedProperty _curve;
        SerializedProperty _speed;
        SerializedProperty _trigger;
        SerializedProperty _event;

        void OnEnable()
        {
            _curve   = serializedObject.FindProperty("_curve");
            _speed   = serializedObject.FindProperty("_speed");
            _trigger = serializedObject.FindProperty("_trigger");
            _event   = serializedObject.FindProperty("_event");
        }

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
}
