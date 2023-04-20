using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AttackIndicator))]
public class AttackIndicatorEditor : Editor
{
    private SerializedProperty _shapeProperty;
    private SerializedProperty _float1Property;
    private SerializedProperty _float2Property;

    private void OnEnable()
    {
        _shapeProperty = serializedObject.FindProperty("shape");
        _float1Property = serializedObject.FindProperty("width");
        _float2Property = serializedObject.FindProperty("radius");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(_shapeProperty);

        switch ((AttackIndicator.shapes)_shapeProperty.enumValueIndex)
        {
            case AttackIndicator.shapes.circle:
                EditorGUILayout.PropertyField(_float2Property);
                break;

            case AttackIndicator.shapes.line:
                EditorGUILayout.PropertyField(_float1Property);
                break;

            default:
                break;
        }

        serializedObject.ApplyModifiedProperties();
    }
}