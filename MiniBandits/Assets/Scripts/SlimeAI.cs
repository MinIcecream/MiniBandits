using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAI : EnemyAI, IDamageable
{ 
    public int chaseSpeed; 

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
           
        Vector2 dir = player.transform.position - transform.position;

        transform.position = (Vector2)transform.position + dir.normalized * chaseSpeed * Time.deltaTime;
         
    } 
    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            coll.gameObject.GetComponent<Health>().DealDamage(30);
        }
    }
}
