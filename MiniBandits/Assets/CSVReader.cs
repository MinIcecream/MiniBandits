using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CSVReader : MonoBehaviour
{
    public TextAsset armorData;
    public TextAsset weaponData;

    void Awake()
    {
      //  ReadArmorCSV();
      //  ReadWeaponCSV();
    }
    void ReadArmorCSV()
    {
        string[] data = armorData.text.Split(new string[] { ",", "\n" }, StringSplitOptions.None);

        int tableSize = data.Length / 17 - 1;

        for ( int i = 0; i<tableSize; i++)
        {
            string itemName = data[17 * (i + 1)];

            Armor item = null;

            if (itemName.Contains("+"))
            { 
                item = (Armor)Resources.Load("Items/Armor/Upgrades/" + itemName);
            }
            else
            { 
                item = (Armor)Resources.Load("Items/Armor/BaseArmor/" + itemName);
            }

            if(item != null)
            {  
                item.health = ParseToInt(data[17 * (i + 1)+4]);
                item.defense = ParseToInt(data[17 * (i + 1) + 5]);
                item.strength = ParseToInt(data[17 * (i + 1) + 6]);
                item.crit = ParseToInt(data[17 * (i + 1) + 7]);
                item.lifeSteal = ParseToInt(data[17 * (i + 1) + 8]);
                item.speed = ParseToInt(data[17 * (i + 1) + 9]);
                item.attackSpeed = ParseToFloat(data[17 * (i + 1) + 10]);
                item.numProjectiles = ParseToInt(data[17 * (i + 1) + 11]);
                item.projectileSpeed = ParseToInt(data[17 * (i + 1) + 12]);
                item.range = ParseToInt(data[17 * (i + 1) + 13]);
                item.AOE = ParseToInt(data[17 * (i + 1) + 14]);
                item.knockBack = ParseToInt(data[17 * (i + 1) + 15]);
                item.description = data[17 * (i + 1) + 16];
            }
            else
            {
                Debug.Log(itemName);
            }
        }
    }
     
    void ReadWeaponCSV()
    {
        string[] data = weaponData.text.Split(new string[] { ",", "\n" }, StringSplitOptions.None);

        int tableSize = data.Length / 11 - 1;

        for (int i = 0; i < tableSize; i++)
        {
            string itemName = data[11 * (i + 1)]; 
            
            Weapon item = null;

            if (itemName.Contains("+"))
            {
                item = (Weapon)Resources.Load("Items/Weapons/Upgrades/" + itemName);
            }
            else
            {
                item = (Weapon)Resources.Load("Items/Weapons/BaseWeapons/" + itemName);
            }

            if (item != null)
            { 
                item.manualDPS = ParseToInt(data[11 * (i + 1) + 2]); 
                item.damage = ParseToInt(data[11 * (i + 1) + 3]);
                item.attackSpeed = ParseToFloat(data[11 * (i + 1) + 4]);
                item.numProjectiles = ParseToInt(data[11 * (i + 1) + 5]);
                item.projectileSpeed = ParseToInt(data[11 * (i + 1) + 6]);
                item.range = ParseToInt(data[11 * (i + 1) + 7]);
                item.AOE = ParseToInt(data[11 * (i + 1) + 8]);
                item.knockBack = ParseToInt(data[11 * (i + 1) + 9]);
            }
            else
            {
                Debug.Log(itemName);
            }
        }
    }

    int ParseToInt(string str)
    {
        if (str == "")
        {
            return 0;
        }
        float f = float.Parse(str);
        return (int)f; 
    }
    float ParseToFloat(string str)
    {
        if (str == "")
        {
            return 0;
        }
        return float.Parse(str);
    }
}
