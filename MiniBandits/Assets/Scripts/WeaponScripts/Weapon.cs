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


    [SerializeField] public int manualDPS; 
    [SerializeField] public int numProjectiles;
    [SerializeField] public int AOE;
    [SerializeField] public int projectileSpeed; 

    [System.Flags] public enum Stats
    {
        manualDPS = 1,
        numProjectiles = 2,
        AOE = 4,
        projectileSpeed = 8
    }
    public Stats stats;
     
}
