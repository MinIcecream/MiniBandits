using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class Item : ScriptableObject
{
    public int id;
    public string displayName;
    public string referenceName;
    public Sprite sprite;
    public int cost;
    public itemRarity rarity;
    public itemType type;
    public int tier;
    public string description;
    public enum itemType
    {
        weapon,
        helmet,
        chestplate,
        pants,
        potion,
        basic
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
