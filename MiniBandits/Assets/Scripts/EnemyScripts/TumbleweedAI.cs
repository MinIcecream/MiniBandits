using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TumbleweedAI : EnemyAI, IDamageable, IAffectable
{  
    public new GameObject collider;

    public float chargeSpeed; 
     

    Rigidbody2D rb;

    float currentRotation;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
     
    IEnumerator AttackTimer()
    {   
        if (player != null)
        { 
            collider.SetActive(true);
            Vector2 chargeDir = player.transform.position - transform.position;
            GetComponent<AttackIndicator>().GenerateAttackIndicator(chargeDir.normalized); 

            yield return new WaitForSeconds(1f);
            rb.AddForce(chargeDir * 50 * chargeSpeed); 
        }
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    public override void Update()
    {
        base.Update();
        player = GameObject.FindWithTag("Player");

        if (player == null)
        {
            return;
        }   
        if (rb.velocity.magnitude <= .01f)
        { 
            if (canAttack)
            {
                canAttack = false;
                attackCoroutine = StartCoroutine(AttackTimer());
            }
        } 
        float linearVelocity = rb.velocity.magnitude; // Get the magnitude of the object's velocity
        float rotationFactor = linearVelocity / .03f; // Calculate the rotation factor based on the linear velocity and rotation speed
        currentRotation += rotationFactor * Time.deltaTime; 
        
        transform.rotation = Quaternion.Euler(0f, 0f, currentRotation);
    } 
    public override void Knockback(float magnitude, Vector2 src)
    { 
        base.Knockback(magnitude, src); 
    }
    public void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            coll.gameObject.GetComponent<IDamageable>().Damage(damage);
        }
    }
}
