using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderAI : EnemyAI, IAffectable
{
    public GameObject projectile;
    
    public bool charging = false;

    public new GameObject collider;

    public float chargeSpeed;
    public float chargeCooldown;

    bool chargeCooldownStarted;

    Rigidbody2D rb;

    bool levelStarted;

    public bool emitWebs;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public override void StartLevel()
    {
        levelStarted = true;
        StartCoroutine(EmitWebs());
    }

    IEnumerator AttackTimer(Vector2 targetPos)
    {  
        if (player != null)
        {
            collider.SetActive(true);
            emitWebs = true; 
            Vector2 direction = (targetPos - (Vector2)transform.position).normalized;

            while (Vector2.Distance(transform.position,targetPos) > 0.1f)
            {   
                transform.position += (Vector3)(direction * chargeSpeed * Time.deltaTime);
               
                yield return null;
            }
            emitWebs = false;
            collider.SetActive(false);
        }
        yield return new WaitForSeconds(chargeCooldown);
        charging = false;
    }

    public override void Update()
    {
        base.Update();
        player = GameObject.FindWithTag("Player");

        if (player == null || !levelStarted)
        {
            return;
        }
        if (!charging)
        {
            charging = true;
            StartCoroutine(AttackTimer(player.transform.position));
        }
    }
    public override void Knockback(float magnitude, Vector2 src)
    {
        if (!charging)
        {
            base.Knockback(magnitude, src);
        }
    }
    IEnumerator EmitWebs()
    {
        while (true)
        {
            if (emitWebs)
            {
                Instantiate(projectile, transform.position, Quaternion.identity); 
            }
            yield return new WaitForSeconds(Random.Range(0.05f,0.11f));
        }
    }
}
