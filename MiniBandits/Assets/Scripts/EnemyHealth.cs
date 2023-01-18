using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    public override void DealDamage(int damage)
    {
        health -= damage;

        GameObject player = GameObject.FindWithTag("Player");
        if (player == null)
        {
            return;
        }
        int lifeSteal = player.GetComponent<Player>().lifeSteal;
        int healAmount = damage * lifeSteal / 100; 
        player.GetComponent<Health>().Heal(healAmount);
    }
}
