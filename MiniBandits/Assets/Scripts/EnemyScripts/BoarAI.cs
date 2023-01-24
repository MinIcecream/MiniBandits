using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarAI : EnemyAI, IDamageable
{
    [HideInInspector]
    public bool ranIntoWall = false;
    [HideInInspector]
    public bool charging = false;

    Vector2 chargeDir;
    public new GameObject collider;

    public float chargeSpeed;
    public float chargeCooldown;

    public override void StartLevel()
    { 
        StartCoroutine(AttackTimer());
    } 
    IEnumerator AttackTimer()
    {
        yield return new WaitForSeconds(chargeCooldown);
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
}
