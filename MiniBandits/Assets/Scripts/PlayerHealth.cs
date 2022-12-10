using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using EZCameraShake;

public class PlayerHealth : Health
{
    public Player player;

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
        CameraShaker.Instance.ShakeOnce(4f, 4f, .1f, .1f);
        base.DealDamage(damage);
        StartCoroutine(DamageAnimation());
    }
    IEnumerator DamageAnimation()
    {
        transform.localScale = new Vector2(2.2f, 2.2f);

        yield return new WaitForSeconds(0.1f);

        transform.localScale = new Vector2(2f, 2f);
    }
}
