using UnityEngine;
using UnityEditor;

namespace Khoreo
{
    [CustomEditor(typeof(AudioThreshold)), CanEditMultipleObjects]
    sealed class AudioThresholdEditor : Editor
    {
        SerializedProperty _tracker;
        SerializedProperty _threshold;
        SerializedProperty _delay;
        SerializedProperty _onEvent;
        SerializedProperty _offEvent;

        void OnEnable()
        {
            _tracker   = serializedObject.FindProperty("_tracker");
            _threshold = serializedObject.FindProperty("<Threshold>k__BackingField");
            _delay     = serializedObject.FindProperty("<Delay>k__BackingField");
            _onEvent   = serializedObject.FindProperty("_onEvent");
            _offEvent  = serializedObject.FindProperty("_offEvent");
        }

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
}
