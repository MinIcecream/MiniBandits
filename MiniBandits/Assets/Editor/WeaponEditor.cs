using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(Weapon))]
public class WeaponEditor : Editor
{
    private SerializedProperty statsProperty;
    private SerializedProperty numProjectilesProperty;
    private SerializedProperty AOEProperty;
    private SerializedProperty projectileSpeedProperty;
    private SerializedProperty manualDPSProperty;
    private SerializedProperty rangeProperty;
    private SerializedProperty knockBackProperty;
     
    private void OnEnable()
    {
        statsProperty = serializedObject.FindProperty("stats");
        numProjectilesProperty = serializedObject.FindProperty("numProjectiles");
        AOEProperty = serializedObject.FindProperty("AOE");
        projectileSpeedProperty = serializedObject.FindProperty("projectileSpeed");
        manualDPSProperty = serializedObject.FindProperty("manualDPS");
        rangeProperty = serializedObject.FindProperty("range");
        knockBackProperty = serializedObject.FindProperty("knockBack");
    }

    public override void OnInspectorGUI()
    {  
        serializedObject.Update();

        EditorGUILayout.PropertyField(serializedObject.FindProperty("displayName"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("referenceName"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("sprite"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("rarity"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("tier"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("type"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("description"));

        EditorGUILayout.PropertyField(statsProperty);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("damage"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("attackSpeed"));

        int intValue = statsProperty.intValue;
        Weapon.Stats test = (Weapon.Stats)statsProperty.intValue;
        test = (Weapon.Stats)Enum.ToObject(typeof(Weapon.Stats), intValue);

        if (test.HasFlag(Weapon.Stats.numProjectiles))
        { 
            EditorGUILayout.PropertyField(numProjectilesProperty);
        }
        if (test.HasFlag(Weapon.Stats.AOE))
        {
            EditorGUILayout.PropertyField(AOEProperty);
        }
        if (test.HasFlag(Weapon.Stats.projectileSpeed))
        {
            EditorGUILayout.PropertyField(projectileSpeedProperty);
        }
        if (test.HasFlag(Weapon.Stats.manualDPS))
        {
            EditorGUILayout.PropertyField(manualDPSProperty);
        }
        if (test.HasFlag(Weapon.Stats.range))
        {
            EditorGUILayout.PropertyField(rangeProperty);
        }
        if (test.HasFlag(Weapon.Stats.knockBack))
        {
            EditorGUILayout.PropertyField(knockBackProperty);
        }

        serializedObject.ApplyModifiedProperties();
    }
}