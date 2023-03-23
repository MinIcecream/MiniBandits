using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TumbleweedAI : EnemyAI, IDamageable, IAffectable
{ 
    [HideInInspector]
    public bool charging = false;
     
    public new GameObject collider;

    public float chargeSpeed;
    public float chargeCooldown;

    bool chargeCooldownStarted;

    Rigidbody2D rb;

    bool levelStarted;

    public float currentRotation;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public override void StartLevel()
    {
        levelStarted = true;
    }

    IEnumerator AttackTimer()
    { 
        yield return new WaitForSeconds(chargeCooldown);

        if (player != null)
        { 
            collider.SetActive(true);
            Vector2 chargeDir = player.transform.position - transform.position; 
            rb.AddForce(chargeDir * 50 * chargeSpeed);
            chargeCooldownStarted = false;
        }
    }

    public override void Update()
    {
        base.Update();
        player = GameObject.FindWithTag("Player");

        if (player == null||!levelStarted)
        {
            return;
        }   
        if (rb.velocity.magnitude <= .01f)
        { 
            if (!chargeCooldownStarted)
            {
                chargeCooldownStarted = true;
                StartCoroutine(AttackTimer());
            }
        } 
        float linearVelocity = rb.velocity.magnitude; // Get the magnitude of the object's velocity
        float rotationFactor = linearVelocity / .03f; // Calculate the rotation factor based on the linear velocity and rotation speed
        currentRotation += rotationFactor * Time.deltaTime; 
        
        transform.rotation = Quaternion.Euler(0f, 0f, currentRotation);
    } 
    public override void Knockback(float magnitude, Vector2 src)
    {
        if (!charging)
        {
            base.Knockback(magnitude, src);
        }
    }
}
