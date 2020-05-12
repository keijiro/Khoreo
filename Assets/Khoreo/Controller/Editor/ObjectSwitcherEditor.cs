using UnityEngine;
using UnityEditor;

namespace Khoreo
{
    [CustomPropertyDrawer(typeof(ObjectSwitcher))]
    sealed class ObjectSwitcherEditor : PropertyDrawer
    {
        static class Styles
        {
            public readonly static GUIContent
              Target = new GUIContent("Target");
        }

        public override float
          GetPropertyHeight(SerializedProperty prop, GUIContent label)
        {
            var h = EditorGUIUtility.singleLineHeight;
            var s = EditorGUIUtility.standardVerticalSpacing;

            var targetType = prop.FindPropertyRelative("_targetType");

            // Multiple edit: Three rows
            if (targetType.hasMultipleDifferentValues) return h * 3 + s * 2;

            // Subtree: Single row
            if (targetType.enumValueIndex
                  == (int)ObjectSwitcher.TargetType.Subtree) return h;

            // Default: Two rows
            return h * 2 + s;
        }

        public override void
          OnGUI(Rect rect, SerializedProperty prop, GUIContent label)
        {
            var h = EditorGUIUtility.singleLineHeight;
            var s = EditorGUIUtility.standardVerticalSpacing;

            var typeField = prop.FindPropertyRelative("_targetType");
            var showAll = typeField.hasMultipleDifferentValues;
            var type = (ObjectSwitcher.TargetType)typeField.enumValueIndex;

            // Prologue
            EditorGUI.BeginProperty(rect, label, prop);
            rect = EditorGUI.PrefixLabel
              (rect, GUIUtility.GetControlID(FocusType.Passive), label);
            rect.height = h;

            // Target type selector
            EditorGUI.PropertyField(rect, typeField, GUIContent.none);
            rect.y += h + s;

            // Target game object field
            if (showAll || type == ObjectSwitcher.TargetType.GameObject)
            {
                var field = prop.FindPropertyRelative("_targetGameObject");
                EditorGUI.PropertyField(rect, field, GUIContent.none);
                rect.y += h + s;
            }

            // Target behaviour field
            if (showAll || type == ObjectSwitcher.TargetType.Behaviour)
            {
                var field = prop.FindPropertyRelative("_targetBehaviour");
                EditorGUI.PropertyField(rect, field, GUIContent.none);
            }

            EditorGUI.EndProperty();
        }
    }
}
