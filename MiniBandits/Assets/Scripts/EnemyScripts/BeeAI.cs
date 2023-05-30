using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeAI : EnemyAI, IAffectable
{
    public GameObject projectile; 
    public int chaseSpeed; 
    public float attackDistance;

    enum states
    {
        chasing,
        firing,
        wandering
    }
    states state = states.wandering;

    public override void Awake()
    {
        base.Awake();
        player = GameObject.FindWithTag("Player");
    } 

    public override void Update()
    {
        base.Update();

        if (player == null)
        {
            return;
        }
        if (state == states.wandering)
        {
            if (Vector2.Distance(player.transform.position, transform.position) < attackDistance)
            {
                state = states.firing;
            }
            if (player)
            {
                state = states.chasing;
            }
        }
        if (state == states.firing && canAttack)
        {
            if (!player)
            {
                state = states.wandering;
            }
            if (Vector2.Distance(player.transform.position, transform.position) > attackDistance)
            {
                state = states.chasing;
            }
        }
        if (state == states.chasing)
        {
            if (!player)
            {
                state = states.wandering;
            }
            if (Vector2.Distance(player.transform.position, transform.position) < attackDistance)
            {
                state = states.firing;
            }
        }

        if (state == states.wandering)
        {
            return;
        }
        if (state == states.firing)
        {
            if (canAttack)
            {
                attackCoroutine=StartCoroutine(Fire());
            }
        }
        if (state == states.chasing)
        {
            if (!canMove)
            {
                return;
            }
            Vector2 dir = player.transform.position - transform.position;

            transform.position = (Vector2)transform.position + dir.normalized * chaseSpeed * Time.deltaTime;
        }
    }
    IEnumerator Fire()
    {
        canAttack = false;

        yield return new WaitForSeconds(attackCooldown);
        //makes projectile
        if (player)
        {
            var newProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
            //shoots projectile at player position
            newProjectile.GetComponent<BaseProjectile>().damage = damage;
            newProjectile.GetComponent<BaseProjectile>().SetDir(((Vector2)(player.transform.position))); 
        } 

        canAttack = true;
    }
}
