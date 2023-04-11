using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAI : EnemyAI, IAffectable
{
    public int chaseSpeed;
    bool canStart = false;
    public override void StartLevel()
    {
        canStart = true;
    }
    public override void Awake()
    {
        base.Awake();
        player = GameObject.FindWithTag("Player");
    }
    public override void Update()
    {
        base.Update();

        if (player == null||!canStart)
        {
            return;
        }

        if (!canMove)
        {
            return;
        }
        Vector2 dir = player.transform.position - transform.position;
        transform.position = (Vector2)transform.position + dir.normalized * chaseSpeed * Time.deltaTime;

    }
    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            Knockback(2, coll.gameObject.transform.position);
            coll.gameObject.GetComponent<Health>().DealDamage(damage);
        }
    } 
}
