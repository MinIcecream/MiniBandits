using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeDroneAI : EnemyAI, IAffectable
{ 
    public int chaseSpeed; 
    public float attackDistance;
    public Collider2D weaponCollider; 

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
            if (Vector2.Distance(player.transform.position, new Vector2(transform.position.x, transform.position.y - 1)) > attackDistance)
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
            if (Vector2.Distance(player.transform.position, new Vector2(transform.position.x, transform.position.y - 1)) < attackDistance)
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
                attackCoroutine = StartCoroutine(Fire());
            }
        }
        if (state == states.chasing)
        {
            if (!canMove)
            {
                return;
            }
            Vector2 dir = (Vector2)player.transform.position - new Vector2(transform.position.x,transform.position.y-1);

            transform.position = (Vector2)transform.position + dir.normalized * chaseSpeed * Time.deltaTime;
        }
    }
    IEnumerator Fire()
    {
        yield return new WaitForSeconds(0.2f);
        GetComponent<Animator>().SetTrigger("Attack"); 
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            coll.gameObject.GetComponent<Health>().DealDamage(damage);
        }
    }
    public override IEnumerator Stagger()
    {
        if (attackCoroutine == null)
        {
            yield break;
        }
        StopCoroutine(attackCoroutine);
        GetComponent<Animator>().CrossFade("Idle", 0f);
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
}
