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

        base.OnInspectorGUI();

        /*
        serializedObject.Update();

        EditorGUILayout.PropertyField(serializedObject.FindProperty("displayName"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("referenceName"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("sprite"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("rarity"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("tier"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("type"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("description"));

        EditorGUILayout.PropertyField(statsProperty);

        DisplayStats(); 
        */
        if (GUILayout.Button("Create Upgrade"))
        {
            Armor script = (Armor)target;
            script.CreateUpgrade();
        }

        serializedObject.ApplyModifiedProperties();
    }

    void DisplayStats()
    {
        int intValue = statsProperty.intValue;
        Armor.Stats test = (Armor.Stats)statsProperty.intValue;
        test = (Armor.Stats)Enum.ToObject(typeof(Armor.Stats), intValue);

        if (test.HasFlag(Armor.Stats.lifeSteal))
        {
            EditorGUILayout.PropertyField(lifeStealProperty);
        }
        else
        {
            lifeStealProperty.intValue = 0;
        }

        if (test.HasFlag(Armor.Stats.health))
        {
            EditorGUILayout.PropertyField(healthProperty);
        }
        else
        {
            healthProperty.intValue = 0;
        }

        if (test.HasFlag(Armor.Stats.defense))
        {
            EditorGUILayout.PropertyField(defenseProperty);
        }
        else
        {
            defenseProperty.intValue = 0;
        }

        if (test.HasFlag(Armor.Stats.strength))
        {
            EditorGUILayout.PropertyField(strengthProperty);
        }
        else
        {
            strengthProperty.intValue = 0;
        }

        if (test.HasFlag(Armor.Stats.crit))
        {
            EditorGUILayout.PropertyField(critProperty);
        }
        else
        {
            critProperty.intValue = 0;
        }

        if (test.HasFlag(Armor.Stats.speed))
        {
            EditorGUILayout.PropertyField(speedProperty);
        }
        else
        {
            speedProperty.intValue = 0;
        }

        if (test.HasFlag(Armor.Stats.numProjectiles))
        {
            EditorGUILayout.PropertyField(numProjectilesProperty);
        }
        else
        {
            numProjectilesProperty.intValue = 0;
        }

        if (test.HasFlag(Armor.Stats.projectileSpeed))
        {
            EditorGUILayout.PropertyField(projectileSpeedProperty);
        }
        else
        {
            projectileSpeedProperty.intValue = 0;
        }

        if (test.HasFlag(Armor.Stats.AOE))
        {
            EditorGUILayout.PropertyField(AOEProperty);
        }
        else
        {
            AOEProperty.intValue = 0;
        }

        if (test.HasFlag(Armor.Stats.range))
        {
            EditorGUILayout.PropertyField(rangeProperty);
        }
        else
        {
            rangeProperty.intValue = 0;
        }

        if (test.HasFlag(Armor.Stats.knockBack))
        {
            EditorGUILayout.PropertyField(knockBackProperty);
        }
        else
        {
            knockBackProperty.intValue = 0;
        }

        if (test.HasFlag(Armor.Stats.attackSpeed))
        {
            EditorGUILayout.PropertyField(attackSpeedProperty);
        }
        else
        {
            attackSpeedProperty.floatValue = 0;
        }
    }
}