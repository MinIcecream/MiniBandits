using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StatsUI : MonoBehaviour
{
    Player player;

    [SerializeField]
    TextMeshProUGUI lifeSteal, defense, speed, strength, health, crit, luck, combatPower, numProjectiles, projectileSpeed, range, AOE, knockBack, attackSpeed;
     
    void Awake()
    { 
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    void Update()
    {
        if (player == null)
        {
            return;
        }
        combatPower.text = "Combat Power: " + player.combatPower.ToString();
        lifeSteal.text = "Lifesteal: " + player.lifeSteal.ToString();
        defense.text = "Defense: " + player.defense.ToString();
        speed.text = "Speed: " + player.speed.ToString();
        strength.text = "Strength: " + player.strength.ToString();
        health.text = "Health: " + player.health.ToString();
        crit.text = "Crit: " + player.crit.ToString(); 
        numProjectiles.text = "Additional Projectiles: " + player.numProjectiles.ToString();
        range.text = "Bonus Range: " + player.range.ToString();
        attackSpeed.text = "Bonus Attack Speed: " + player.attackSpeed.ToString();
        AOE.text = "Bonus AOE: " + player.AOE.ToString();
        knockBack.text = "Bonus Knockback: " + player.knockBack.ToString();
        projectileSpeed.text = "Bonus Projectile Speed: " + player.projectileSpeed.ToString(); 
    }
}
