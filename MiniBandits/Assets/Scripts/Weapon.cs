using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Item/Weapon")]
public class Weapon : Item
{ 
    public Stat[] weaponStats =
    {
        new Stat("Damage",0),
        new Stat("AttackSpeed",0), 
    };
}
