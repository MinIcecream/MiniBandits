using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Armor", menuName = "Item/Armor")]
public class Armor : Item
{ 
    public Stat[] statsModified =
    {
        new Stat("Lifesteal",0),
        new Stat("Defense",0),
        new Stat("Speed",0),
        new Stat("Strength",0),
        new Stat("Health",0),
        new Stat("Crit",0),
        new Stat("Luck",0),
    }; 
}

