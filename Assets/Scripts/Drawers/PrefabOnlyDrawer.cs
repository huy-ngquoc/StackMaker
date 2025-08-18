namespace Game;

using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(PrefabOnlyAttribute))]
public sealed class PrefabOnlyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var fieldType = this.fieldInfo.FieldType;
        bool isGameObjectField = typeof(GameObject).IsAssignableFrom(fieldType);
        bool isComponentField = typeof(Component).IsAssignableFrom(fieldType);

        if (!isGameObjectField && !isComponentField)
        {
            EditorGUI.PropertyField(position, property, label);
            var r = new Rect(position.x, position.yMax + 2, position.width, EditorGUIUtility.singleLineHeight);
            EditorGUI.HelpBox(r, "PrefabOnly is for GameObject/Component.", MessageType.Warning);
            return;
        }

        EditorGUI.BeginProperty(position, label, property);
        EditorGUI.BeginChangeCheck();

        var picked = EditorGUI.ObjectField(
            position,
            label,
            property.objectReferenceValue,
            isGameObjectField ? typeof(GameObject) : typeof(Object),
            /* allowSceneObjects: */ false);

        if (EditorGUI.EndChangeCheck())
        {
            if (picked == null)
            {
                property.objectReferenceValue = null;
            }
            else
            {
                bool isPrefabAsset = PrefabUtility.IsPartOfPrefabAsset(picked);

                if ((!isPrefabAsset) && (picked is GameObject go))
                {
                    isPrefabAsset = PrefabUtility.IsPartOfPrefabAsset(go);
                }

                if (!isPrefabAsset)
                {
                    Debug.LogError($"{picked.name} is not a prefab asset. Please drag a prefab from the project.");
                }
                else
                {
                    if (isComponentField)
                    {
                        if (picked is GameObject goPrefab)
                        {
                            var comp = goPrefab.GetComponent(fieldType);
                            if (comp != null)
                            {
                                picked = comp;
                            }
                            else
                            {
                                Debug.LogError($"Prefab '{goPrefab.name}' has no component of type {fieldType.Name}.");
                                picked = null;
                            }
                        }
                        else if (picked is not Component)
                        {
                            Debug.LogError($"Selected value is not valid for field {fieldType.Name}.");
                            picked = null;
                        }
                    }

                    if (isGameObjectField && picked is Component c)
                    {
                        picked = c.gameObject;
                    }
                }

                property.objectReferenceValue = picked;
            }
        }

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUIUtility.singleLineHeight;
    }
}
