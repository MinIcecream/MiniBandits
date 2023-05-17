using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class Item : ScriptableObject
{
    public int id;
    public string displayName;
    public string referenceName;
    public Sprite sprite;
    [HideInInspector] public int cost;
    public itemRarity rarity;
    public itemType type;
    public int tier;
    public string description;
    [HideInInspector]public Color color;

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
    void OnEnable()
    {  
        switch (rarity)
        {
            case itemRarity.common:
                color = new Color(255, 255, 255, 1);
                cost = 10;
                break;
            case itemRarity.uncommon:
                color = new Color(0, 242, 0, 1);
                cost = 20;
                break;
            case itemRarity.rare:
                color = new Color(0, 137, 255, 1);
                cost = 30;
                break;
            case itemRarity.epic:
                color = new Color(155, 0, 173, 1);
                cost = 50;
                break;
        } 
    }
}
