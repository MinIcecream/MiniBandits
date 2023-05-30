using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentinelAI : EnemyAI, IAffectable
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
        //makes projectile
        var newProjectile = Instantiate(projectile, transform.position, Quaternion.identity); 

        canAttack = true;
    }

    public override void Knockback(float m, Vector2 s)
    {
        StartCoroutine(Stagger());
    }
}
