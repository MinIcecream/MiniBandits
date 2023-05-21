using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Armor", menuName = "Item/Armor")]
public class Armor : Item
{
    [Space(10)]
    [Header("Stats")]
    public int lifeSteal;
    public int defense;
    public int speed;
    public int strength;
    public int health;
    public int crit; 
    public int numProjectiles;
    public int AOE;
    public int projectileSpeed;
    public int range; 
    public float attackSpeed; 
    public int knockBack; 


     
    [System.Flags] public enum Stats
    {
        numProjectiles = 1,
        AOE = 2,
        projectileSpeed = 4,
        range = 8,
        knockBack = 16,
        lifeSteal = 32,
        defense = 64,
        speed = 128,
        strength = 256,
        health = 512,
        crit = 1024,
        attackSpeed = 2048
    } 
    public Stats stats;

    public void CreateUpgrade()
    {
        Armor newWeapon = Instantiate(this);

        newWeapon.tier++;
        newWeapon.displayName += "+";

        string newRefName = newWeapon.referenceName;

        for (int i = 0; i < newWeapon.displayName.Length; i++)
        {
            if (newWeapon.displayName[i] == '+')
            {
                newRefName += '+';
            }
        }
        string path = "Assets/Resources/Items/Armor/BaseArmor/" + newRefName + ".asset";
        UnityEditor.AssetDatabase.CreateAsset(newWeapon, path);
        UnityEditor.AssetDatabase.SaveAssets();
        UnityEditor.AssetDatabase.Refresh();
    }
}

