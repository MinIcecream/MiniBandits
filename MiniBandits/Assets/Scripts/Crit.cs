using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crit : MonoBehaviour
{
    public static int CalculateCrit(int rawDamage)
    {
        Player player = GameObject.FindWithTag("Player").GetComponent<Player>();

        int damage=rawDamage;

        if (player == null)
        {
            return rawDamage;
        }

        int critChance = player.crit;
         
        int ran = Random.Range(0, 100);

        if (ran <= critChance && critChance!=0)
        {
            //Crit
            damage= (int)(rawDamage * 1.5f);
        }
        if (critChance > 100)
        {
            damage += critChance - 100;
        } 
        return damage;
        
    } 
}
