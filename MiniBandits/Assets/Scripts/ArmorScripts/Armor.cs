using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Armor", menuName = "Item/Armor")]
public class Armor : Item
{
    [Space(10)]
    [Header("Stats")]
    public int lifeSteal;
    public int defense;
    public int speed;
    public int strength;
    public int health;
    public int crit;
    public int luck;
     
}

