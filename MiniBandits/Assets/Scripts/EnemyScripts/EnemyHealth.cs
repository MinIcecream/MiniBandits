using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{ 
    public override void DealDamage(int damage)
    {
        GameObject player = GameObject.FindWithTag("Player"); 

        if (player == null)
        { 
            health -= damage;
            return;
        }
          
        //Handle Crit
        int finalDamage = Crit.CalculateCrit(damage);
        health -= finalDamage;
        if (finalDamage != damage)
        { 
            PopupManager.SpawnPopup(transform.position, finalDamage.ToString(), true);
        }
        else
        { 
            PopupManager.SpawnPopup(transform.position, finalDamage.ToString(), false); 
        }
        //Handle Lifesteal
        Player stats = player.GetComponent<Player>();
        int lifeSteal = stats.lifeSteal;
        int healAmount = finalDamage * lifeSteal / 100;
        player.GetComponent<Health>().Heal(healAmount);
    }
     
}
