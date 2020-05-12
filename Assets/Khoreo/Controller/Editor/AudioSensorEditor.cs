using UnityEngine;
using UnityEditor;

namespace Khoreo
{
    [CustomEditor(typeof(AudioSensor)), CanEditMultipleObjects]
    sealed class AudioSensorEditor : Editor
    {
        SerializedProperty _tracker;
        SerializedProperty _threshold;
        SerializedProperty _delay;
        SerializedProperty _target;

        void OnEnable()
        {
            _tracker   = serializedObject.FindProperty("_tracker");
            _threshold = serializedObject.FindProperty("_threshold");
            _delay     = serializedObject.FindProperty("_delay");
            _target    = serializedObject.FindProperty("_target");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(_tracker);
            EditorGUILayout.PropertyField(_threshold);
            EditorGUILayout.PropertyField(_delay);
            EditorGUILayout.PropertyField(_target);
            serializedObject.ApplyModifiedProperties();
        }
    }
}
