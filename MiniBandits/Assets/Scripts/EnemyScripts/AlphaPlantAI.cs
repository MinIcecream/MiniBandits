using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaPlantAI : EnemyAI, IDamageable, IAffectable
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
        if (player == null)
        {
            yield break;
        }
        Vector2 dir = ((Vector2)(player.transform.position - transform.position)).normalized;
        for (int i = 0; i < 4; i++)
        {
            //makes projectile
            var newProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
            //shoots projectile at player position
            Vector2 newVector = Quaternion.Euler(0, 0, 90 * i) * dir;
            newProjectile.GetComponent<BaseProjectile>().damage = damage;
            newProjectile.GetComponent<BaseProjectile>().SetDir(newVector + (Vector2)transform.position);
            //waits 1 second before shooting another
        }
        canAttack = true;
    }

    public override void Knockback(float m, Vector2 s)
    {
        StartCoroutine(Stagger());
    }
}