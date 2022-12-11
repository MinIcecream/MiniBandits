using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class Item : ScriptableObject
{
    public int id;
    public new string name;
    public Sprite sprite;
    public itemRarity rarity;
    public itemType type;
    public int tier;  

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
