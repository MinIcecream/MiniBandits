using System.Collections;
using System.Collections.Generic;
using UnityEngine;  

public class PlayerHealth : Health
{
    public Player player;
    bool canDamage = true;

    public override void Awake()
    { 
        player = GetComponent<Player>();
        maxHealth =  player.GetHealth(); 
        base.Awake(); 
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
            MakeInvincible();
            canDamage = false;
            health -= (int)(damage * (100.0 / (100 + player.defense*2)));
        } 
    }
    public void MakeInvincible()
    { 
        canDamage = false;
        this.gameObject.layer = LayerMask.NameToLayer("OnlyWithTerrain");
        StartCoroutine(InvulnerabilityFramesCooldown());
    }
    IEnumerator InvulnerabilityFramesCooldown()
    {
        yield return new WaitForSeconds(0.5f);
        this.gameObject.layer = LayerMask.NameToLayer("Player");
        canDamage = true;
    }
}
