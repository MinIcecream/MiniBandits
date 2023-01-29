using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Market : MonoBehaviour
{
    public MarketItem[] items;
    void Start()
    {
        for(int i = 0; i < 3; i++)
        {
            if (Random.Range(0, 1) == 1)
            {
                items[i].item=RoomOptionGenerator.GenerateRandomArmor();
            }
            else
            { 
                items[i].item=RoomOptionGenerator.GenerateRandomWeapon();
            }
        }
    } 
}
