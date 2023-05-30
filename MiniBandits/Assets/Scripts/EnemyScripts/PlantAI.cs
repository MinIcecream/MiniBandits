using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantAI : EnemyAI, IAffectable
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
        //shoots projectile at player position
        newProjectile.GetComponent<BaseProjectile>().damage = damage;
        newProjectile.GetComponent<BaseProjectile>().SetDir(((Vector2)player.transform.position));
        //waits 1 second before shooting another

        canAttack = true;
    }

    public override void Knockback(float m, Vector2 s)
    {
        StartCoroutine(Stagger());
    }
}
