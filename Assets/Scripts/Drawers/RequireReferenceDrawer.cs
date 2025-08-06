#if UNITY_EDITOR

#nullable enable

namespace Game;

using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(RequireReferenceAttribute))]
public sealed class RequireReferenceDrawer : PropertyDrawer
{
    private bool hierarchyChanged = false;

    public RequireReferenceDrawer()
    {
        // Register an event that triggers when the hierarchy changes (e.g., component added/removed)
        EditorApplication.hierarchyChanged += () => this.hierarchyChanged = true;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Start checking for changes in the Inspector
        EditorGUI.BeginChangeCheck();

        EditorGUI.PropertyField(position, property, label, true);

        if (EditorGUI.EndChangeCheck() || this.hierarchyChanged)
        {
            this.RequireReferenceLogic(property);
            this.hierarchyChanged = false; // Reset flag
        }
    }

    private void RequireReferenceLogic(SerializedProperty property)
    {
        var targetObject = property.serializedObject.targetObject;

        // Only works with fields that their type is Object (Component, GameObject, ScriptableObject, ...)
        if (property.propertyType != SerializedPropertyType.ObjectReference)
        {
            Debug.LogError($"`{nameof(ResolveComponentAttribute)}` is not valid " +
                $"for field `{this.fieldInfo.FieldType}` in Object `{targetObject.name}`!");
            return;
        }

        if (targetObject is not MonoBehaviour monoBehaviour)
        {
            Debug.LogError($"`{nameof(ResolveComponentAttribute)}` is not valid " +
                $"for field `{this.fieldInfo.FieldType}` in Object `{targetObject.name}`!");
            return;
        }

        var fieldInfo = this.fieldInfo;
        var fieldType = fieldInfo.FieldType;
        var fieldName = fieldInfo.Name;

        if (property.objectReferenceValue == null)
        {
            Debug.LogError($"The reference is required in the field `{fieldName}` " +
                $"of type `{fieldType.FullName}` in MonoBehaviour `{monoBehaviour.GetType().FullName}` " +
                $"on GameObject `{monoBehaviour.name}`!");
        }
    }
}

#endif
