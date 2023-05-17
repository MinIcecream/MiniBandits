using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(Armor))]
public class ArmorEditor : Editor
{
    private SerializedProperty statsProperty;
    private SerializedProperty lifeStealProperty;
    private SerializedProperty healthProperty;
    private SerializedProperty defenseProperty;
    private SerializedProperty speedProperty;
    private SerializedProperty critProperty;
    private SerializedProperty strengthProperty;
    private SerializedProperty numProjectilesProperty;
    private SerializedProperty projectileSpeedProperty;
    private SerializedProperty AOEProperty;
    private SerializedProperty knockBackProperty;
    private SerializedProperty rangeProperty;
    private SerializedProperty attackSpeedProperty;

     
    private void OnEnable()
    {
        statsProperty = serializedObject.FindProperty("stats");
        lifeStealProperty = serializedObject.FindProperty("lifeSteal"); 
        healthProperty= serializedObject.FindProperty("health"); 
        defenseProperty = serializedObject.FindProperty("defense"); 
        speedProperty = serializedObject.FindProperty("speed"); 
        critProperty = serializedObject.FindProperty("crit"); 
        strengthProperty = serializedObject.FindProperty("strength");  
        AOEProperty = serializedObject.FindProperty("AOE"); 
        rangeProperty = serializedObject.FindProperty("range"); 
        projectileSpeedProperty = serializedObject.FindProperty("projectileSpeed"); 
        numProjectilesProperty = serializedObject.FindProperty("numProjectiles");  
        knockBackProperty = serializedObject.FindProperty("knockBack");  
        attackSpeedProperty = serializedObject.FindProperty("attackSpeed");  
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

        int intValue = statsProperty.intValue;
        Armor.Stats test = (Armor.Stats)statsProperty.intValue;
        test = (Armor.Stats)Enum.ToObject(typeof(Armor.Stats), intValue);

        if (test.HasFlag(Armor.Stats.lifeSteal))
        { 
            EditorGUILayout.PropertyField(lifeStealProperty);
        }
        if (test.HasFlag(Armor.Stats.health))
        {
            EditorGUILayout.PropertyField(healthProperty);
        }
        if (test.HasFlag(Armor.Stats.defense))
        {
            EditorGUILayout.PropertyField(defenseProperty);
        }
        if (test.HasFlag(Armor.Stats.strength))
        {
            EditorGUILayout.PropertyField(strengthProperty);
        }
        if (test.HasFlag(Armor.Stats.crit))
        {
            EditorGUILayout.PropertyField(critProperty);
        }
        if (test.HasFlag(Armor.Stats.speed))
        {
            EditorGUILayout.PropertyField(speedProperty);
        }
        if (test.HasFlag(Armor.Stats.numProjectiles))
        {
            EditorGUILayout.PropertyField(numProjectilesProperty);
        }
        if (test.HasFlag(Armor.Stats.projectileSpeed))
        {
            EditorGUILayout.PropertyField(projectileSpeedProperty);
        }
        if (test.HasFlag(Armor.Stats.AOE))
        {
            EditorGUILayout.PropertyField(AOEProperty);
        }
        if (test.HasFlag(Armor.Stats.range))
        {
            EditorGUILayout.PropertyField(rangeProperty);
        }
        if (test.HasFlag(Armor.Stats.knockBack))
        {
            EditorGUILayout.PropertyField(knockBackProperty);
        }
        if (test.HasFlag(Armor.Stats.attackSpeed))
        {
            EditorGUILayout.PropertyField(attackSpeedProperty);
        }

        serializedObject.ApplyModifiedProperties();
    }
}