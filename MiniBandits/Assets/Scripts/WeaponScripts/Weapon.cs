using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Item/Weapon")]
public class Weapon : Item
{
    [Space(10)]
    [Header("Stats")] 
    public int damage;
    public float attackSpeed;
    public int manualDPS;
}
