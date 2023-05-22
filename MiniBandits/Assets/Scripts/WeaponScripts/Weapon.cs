using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Item/Weapon")]
public class Weapon : Item
{
    [Space(10)]
    [Header("Stats")]
    public int damage;
    public float attackSpeed;
     
    [SerializeField] public int manualDPS; 
    [SerializeField] public int numProjectiles;
    [SerializeField] public int AOE;
    [SerializeField] public int projectileSpeed;
    [SerializeField] public int range;
    [SerializeField] public int knockBack;
     
    [System.Flags] public enum Stats
    {
        manualDPS = 1,
        numProjectiles = 2,
        AOE = 4,
        projectileSpeed = 8,
        range = 16,
        knockBack = 32
    } 
   // public Stats stats;

    public void CreateUpgrade()
    {
        Weapon newWeapon = Instantiate(this);

        newWeapon.tier++;
        newWeapon.displayName += "+";
         
        string path = "Assets/Resources/Items/Weapons/BaseWeapons/"+newWeapon.displayName+".asset";
        UnityEditor.AssetDatabase.CreateAsset(newWeapon, path);
        UnityEditor.AssetDatabase.SaveAssets();
        UnityEditor.AssetDatabase.Refresh();
    }
}
