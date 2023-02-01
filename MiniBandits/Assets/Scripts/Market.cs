using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Market : MonoBehaviour
{
    public List<MarketItem> items = new List<MarketItem>(); 

    void Start()
    {
        for(int i = 0; i < 3; i++)
        {
            if (Random.Range(0, 2) >= 1)
            {
                Item newItem = RoomOptionGenerator.GenerateRandomArmor();

                while (true)
                {
                    bool canBreak = true;
                    foreach(MarketItem k in items)
                    {
                        if (k.item == newItem)
                        {
                            canBreak= false;
                        }
                    }
                    if (canBreak)
                    {
                        break;
                    }
                    newItem = RoomOptionGenerator.GenerateRandomArmor();
                }
                items[i].item = newItem;
            }
            else
            {  
                Item newItem2 = RoomOptionGenerator.GenerateRandomWeapon();

                while (true)
                {
                    bool canBreak2 = true;
                    foreach (MarketItem j in items)
                    {
                        if (j.item == newItem2)
                        {
                            canBreak2 = false;
                        }
                    }
                    if (canBreak2)
                    {
                        break;
                    }
                    newItem2 = RoomOptionGenerator.GenerateRandomWeapon();
                }
                items[i].item = newItem2;
            }
        }
    } 
}
