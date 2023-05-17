using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShimmeringRing : BaseProjectile
{
    public float distanceTraveled;
    Vector2 prevPosition;
    GameObject player;

    bool canDestroy=false;
    public Collider2D terrainCollider;

    void Start()
    {
        prevPosition = transform.position;
        player = GameObject.FindWithTag("Player"); 
    }

    void Update()
    {
        // Calculate the distance traveled by the object
        distanceTraveled += Vector2.Distance(prevPosition, transform.position);
        prevPosition = transform.position;

        if (player == null)
        {
            return; 
        }
        if (distanceTraveled > range)
        {
            DoIt();
            collider.enabled = false;
            terrainCollider.enabled = false;
            canDestroy = true;
        }
        if (Vector2.Distance(transform.position, player.transform.position) < 0.3f && canDestroy)
        {
            Destroy(gameObject);
        }
    }
     

    void LateUpdate()
    {
        transform.Rotate(new Vector3(0, 0, 1800 * Time.deltaTime));
    }

    void DoIt()
    {
        // Calculate the direction towards the player
        Vector2 directionToPlayer = player.transform.position - transform.position;

        // Calculate the angle between the object's current velocity and the direction towards the player
        float angleToPlayer = Vector2.SignedAngle(rb.velocity, directionToPlayer);

        // Limit the maximum rotation angle
        float rotationAngle = Mathf.Clamp(angleToPlayer, -20, 20);
         
        // Rotate the object's velocity towards the direction to the player
        Vector2 newVelocity = Quaternion.Euler(0f, 0f, rotationAngle) * rb.velocity * Time.deltaTime;

        // Set the magnitude of the new velocity to the original magnitude
        newVelocity = newVelocity.normalized * rb.velocity.magnitude;

        // Set the object's velocity to the new velocity
        rb.velocity = newVelocity;
    }
    public override void OnTriggerEnter2D(Collider2D coll)
    {
        GameObject obj = coll.gameObject;
        if (obj.GetComponent<Health>() != null)
        {
            if (obj.GetComponent<IDamageable>() != null)
            {
                obj.GetComponent<IDamageable>().Damage(damage);
            }
            if (obj.GetComponent<IAffectable>() != null)
            {
                obj.GetComponent<IAffectable>().Knockback(knockBack, transform.position);
            } 
        }

        if (coll.gameObject.tag == "Enemy")
        { 
            Vector2 vp = (Vector2)coll.gameObject.transform.position - (Vector2)transform.position;
            vp.Normalize();

            // Calculate the vector perpendicular to vp
            Vector2 n = new Vector2(-vp.y, vp.x);

            // Calculate the final velocity after the collision
            Vector2 vf = rb.velocity - 2 * Vector2.Dot(rb.velocity - vp, n) * n;

            // Set the object's velocity to the final velocity
            rb.velocity = vf;
        } 
    } 
}
