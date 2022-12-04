using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static List<string> weaponList = new List<string>(); 

    void Awake()
    {
        weaponList.Add("BigSword");
        weaponList.Add("BloodyMachete");
        weaponList.Add("DiamondSword");
        weaponList.Add("NormalSword");
        weaponList.Add("SerratedSword"); 
    }
}
