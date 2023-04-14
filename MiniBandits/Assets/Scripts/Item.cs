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
    public Color color;

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
        uncommon,
        rare,
        epic
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
    void OnEnable()
    {  
        switch (rarity)
        {
            case itemRarity.common:
                color = new Color(255, 255, 255, 1);
                break;
            case itemRarity.uncommon:
                color = new Color(0, 242, 0, 1);
                break;
            case itemRarity.rare:
                color = new Color(0, 137, 255, 1);
                break;
            case itemRarity.epic:
                color = new Color(155, 0, 173, 1);
                break;
        }
    }
}
