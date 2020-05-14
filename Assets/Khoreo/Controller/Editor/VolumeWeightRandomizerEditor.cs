using UnityEngine;
using UnityEditor;

namespace Khoreo
{
    [CustomEditor(typeof(VolumeWeightRandomizer)), CanEditMultipleObjects]
    sealed class VolumeWeightRandomizerEditor : Editor
    {
        SerializedProperty _volume;
        SerializedProperty _trigger;
        SerializedProperty _reset;

        void OnEnable()
        {
            _volume  = serializedObject.FindProperty("_volume");
            _trigger = serializedObject.FindProperty("_trigger");
            _reset   = serializedObject.FindProperty("_reset");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(_volume);

            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(_trigger);

            if (EditorApplication.isPlaying && GUILayout.Button("Trigger"))
                ((VolumeWeightRandomizer)target).Randomize();

            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(_reset);

            if (EditorApplication.isPlaying && GUILayout.Button("Reset"))
                ((VolumeWeightRandomizer)target).SetWeight(0);

            serializedObject.ApplyModifiedProperties();
        }
    }
}
