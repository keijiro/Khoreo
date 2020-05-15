using UnityEditor;

namespace Khoreo
{
    [CustomEditor(typeof(Toggle)), CanEditMultipleObjects]
    sealed class ToggleEditor : Editor
    {
        SerializedProperty _state;
        SerializedProperty _action;
        SerializedProperty _offEvent;
        SerializedProperty _onEvent;

        void OnEnable()
        {
            _state    = serializedObject.FindProperty("_state");
            _action   = serializedObject.FindProperty("_action");
            _offEvent = serializedObject.FindProperty("_offEvent");
            _onEvent  = serializedObject.FindProperty("_onEvent");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(_state);
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(_action);
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(_offEvent);
            EditorGUILayout.PropertyField(_onEvent);
            serializedObject.ApplyModifiedProperties();
        }
    }
}
