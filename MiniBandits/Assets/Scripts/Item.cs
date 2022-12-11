using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="New Item",menuName="Item")]
public class Item : ScriptableObject
{
    public int id;
    public new string name;
    public Sprite sprite;
    public itemRarity rarity;
    public itemType type;
    public int tier;
     
    public Stat[] stats= 
    {  
        new Stat("Lifesteal",0),
        new Stat("Defense",0),
        new Stat("Speed",0),
        new Stat("Damage",0),
        new Stat("Health",0),
        new Stat("Crit",0),
        new Stat("Luck",0), 
    };

    public enum itemType
    {
        weapon,
        helmet,
        chesplate,
        pants,
        potion
    }
    public enum itemRarity
    {
        common,
        rare,
        epic,
        legendary
    }
    [System.Serializable]
    public struct Stat
    {
        public string statName;
        public int value;

        public Stat(string n,int v)
        {
            statName = n;
            value = v;
        }
    }
}
