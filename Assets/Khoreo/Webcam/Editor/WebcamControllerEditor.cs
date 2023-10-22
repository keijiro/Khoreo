using UnityEditor;
using UnityEngine;

namespace Khoreo {

[CustomEditor(typeof(WebcamController))]
sealed class WebcamControllerEditor : Editor
{
    SerializedProperty _deviceName;
    SerializedProperty _threshold;
    SerializedProperty _contrast;

    static GUIContent _selectLabel = new GUIContent("Select");

    void OnEnable()
    {
        _deviceName = serializedObject.FindProperty("_deviceName");
        _threshold = serializedObject.FindProperty("_threshold");
        _contrast = serializedObject.FindProperty("_contrast");
    }

    void ChangeWebcam(string name)
    {
        serializedObject.Update();
        _deviceName.stringValue = name;
        serializedObject.ApplyModifiedProperties();
    }

    void ShowDeviceSelector(Rect rect)
    {
        var menu = new GenericMenu();
        foreach (var device in WebCamTexture.devices)
            menu.AddItem(new GUIContent(device.name), false,
                         () => ChangeWebcam(device.name));
        menu.DropDown(rect);
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUI.BeginDisabledGroup(Application.isPlaying);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PropertyField(_deviceName);
        var rect = EditorGUILayout.GetControlRect(false, GUILayout.Width(60));
        if (EditorGUI.DropdownButton(rect, _selectLabel, FocusType.Keyboard))
            ShowDeviceSelector(rect);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.PropertyField(_threshold);
        EditorGUILayout.PropertyField(_contrast);

        EditorGUI.EndDisabledGroup();

        serializedObject.ApplyModifiedProperties();
    }
}

} // namespace Khoreo
