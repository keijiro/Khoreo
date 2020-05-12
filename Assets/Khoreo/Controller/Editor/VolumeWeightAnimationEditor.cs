using UnityEditor;

namespace Khoreo
{
    [CustomEditor(typeof(VolumeWeightAnimation)), CanEditMultipleObjects]
    sealed class VolumeWeightAnimationEditor : Editor
    {
        SerializedProperty _curve;
        SerializedProperty _speed;
        SerializedProperty _trigger;
        SerializedProperty _volume;

        void OnEnable()
        {
            _curve   = serializedObject.FindProperty("_curve");
            _speed   = serializedObject.FindProperty("_speed");
            _trigger = serializedObject.FindProperty("_trigger");
            _volume  = serializedObject.FindProperty("_volume");
        }

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
}
