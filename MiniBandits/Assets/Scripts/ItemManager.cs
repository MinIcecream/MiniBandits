using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{ 
    public static List<Item> itemList = new List<Item>(); 

    void Awake()
    {
        foreach (Item item in Resources.LoadAll<Item>("Items"))
        {
            itemList.Add(item); 
        }
    }
    public static Item GetRandomItem()
    {
        return itemList[Random.Range(0, itemList.Count - 1)];
    }
}
