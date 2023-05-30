using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarAI : EnemyAI, IDamageable, IAffectable
{
    [HideInInspector]
    public bool ranIntoWall = false;
    [HideInInspector]
    public bool charging = false;

    Vector2 chargeDir;
    public new GameObject collider;

    public float chargeSpeed; 

    public override void StartLevel()
    { 
        StartCoroutine(AttackTimer());
    } 
    IEnumerator AttackTimer()
    {
        yield return new WaitForSeconds(attackCooldown);
        if (player != null)
        {
            collider.SetActive(true);
            chargeDir = player.transform.position - transform.position;
            chargeDir = chargeDir.normalized;
            GetComponent<FacePlayer>().enabled = false;
            charging = true;
        } 
    }

    public override void Update()
    {
        base.Update();
        player = GameObject.FindWithTag("Player");

        if (player == null)
        {
            return;
        }
        if (charging && ranIntoWall)
        {
            GetComponent<FacePlayer>().enabled = true;
            ranIntoWall = false;
            charging = false;
            collider.SetActive(false);
            StartCoroutine(AttackTimer());
        }
        if (charging)
        { 
            transform.position = (Vector2)transform.position + chargeDir * chargeSpeed*Time.deltaTime;  
        }
    } 
    void OnCollisionEnter2D(Collision2D coll)
    { 
        if (coll.gameObject.tag == "Wall")
        { 
            ranIntoWall = true;
        }
    }
    public override void Knockback(float magnitude, Vector2 src)
    {
        if (!charging)
        {
            base.Knockback(magnitude, src);
        } 
    }
     
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            Knockback(2, coll.gameObject.transform.position);
            coll.gameObject.GetComponent<Health>().DealDamage(damage);
        }
    }
}
