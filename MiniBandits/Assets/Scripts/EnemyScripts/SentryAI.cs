using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentryAI : EnemyAI, IAffectable
{
    public GameObject projectile;


    void Update()
    {
        base.Update();
        if (canAttack)
        {
            attackCoroutine = StartCoroutine(Fire());
        }
    }
    IEnumerator Fire()
    {
        player = GameObject.FindWithTag("Player");
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown); 

        for(int i = 0; i < 3; i++)
        { 
            //makes projectile
            var newProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
            //shoots projectile at player position
            if (player == null)
            {
                break;
            }
            newProjectile.GetComponent<BaseProjectile>().damage = damage;
            newProjectile.GetComponent<BaseProjectile>().SetDir(((Vector2)(player.transform.position)));
            //waits 1 second before shooting another
            yield return new WaitForSeconds(0.2f);
        }
        canAttack = true;
    }

    public override void Knockback(float m, Vector2 s)
    { 
        StartCoroutine(Stagger()); 
    }
}
