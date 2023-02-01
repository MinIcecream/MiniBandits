using System.Collections;
using System.Collections.Generic;
using UnityEngine;  

public class PlayerHealth : Health
{
    public Player player;
    bool canDamage = true;

    public override void Start()
    { 
        player = GetComponent<Player>();
        maxHealth =  player.GetHealth(); 
        base.Start(); 
    }
    void Update()
    {
        int healthStat = player.GetHealth();
        int currHealth = GetHealth();
        if (maxHealth != healthStat)
        {
            //If your max health increased:
            if (healthStat > maxHealth)
            {
                int diff = maxHealth - currHealth;
                maxHealth = healthStat;
                SetHealth(maxHealth - diff);
            }
            //If Max health decreased:
            else
            {
                maxHealth = healthStat;
                if (healthStat < currHealth)
                {
                    SetHealth(healthStat);
                }
            }
        } 
    } 
    public override void DealDamage(int damage)
    {
        if (canDamage)
        {
            StartCoroutine(InvulnerabilityFrames());
            canDamage = false;
            health -= (int)(damage * (100.0 / (100 + player.defense)));
        } 
    }
    IEnumerator InvulnerabilityFrames()
    {
        yield return new WaitForSeconds(0.5f);
        canDamage = true;
    }
}
