using UnityEngine;
using UnityEditor;
using KlutterTools.InspectorUtils;

namespace Khoreo {

[CustomEditor(typeof(VolumeWeightRandomizer)), CanEditMultipleObjects]
sealed class VolumeWeightRandomizerEditor : Editor
{
    AutoProperty _volume = null;
    AutoProperty _trigger = null;
    AutoProperty _reset = null;

    void OnEnable()
      => AutoProperty.Scan(this);

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        var randomizer = (VolumeWeightRandomizer)target;

        EditorGUILayout.PropertyField(_volume);
        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(_trigger);
        if (EditorApplication.isPlaying && GUILayout.Button("Trigger"))
            randomizer.Randomize();
        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(_reset);
        if (EditorApplication.isPlaying && GUILayout.Button("Reset"))
            randomizer.SetWeight(0);

        serializedObject.ApplyModifiedProperties();
    }
}

} // namespace Khoreo {
