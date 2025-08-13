#if UNITY_EDITOR

#nullable enable

namespace Game;

using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(TagSelectionAttribute))]
public sealed class TagSelectionDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (property.propertyType == SerializedPropertyType.String)
        {
            property.stringValue = EditorGUI.TagField(position, label, property.stringValue);
        }
        else
        {
            EditorGUI.LabelField(position, label.text, "Use [TagSelector] with string.");
        }
    }
}
#endif
