using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissile : MonoBehaviour
{
    public int damage;
    public float knockBackAmt;
    public GameObject particles;

    public Transform target;

    public float speed = 5f;
    public float rotateSpeed = 200f;

    private Rigidbody2D rb; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  
    }

    void FixedUpdate()
    {
        if (target)
        {
            Vector2 direction = (Vector2)target.position - rb.position;

            direction.Normalize();

            float rotateAmount = Vector3.Cross(direction, transform.up).z;

            rb.angularVelocity = -rotateAmount * rotateSpeed;

            rb.velocity = transform.up * speed;
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
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
                obj.GetComponent<IAffectable>().Knockback(knockBackAmt, transform.position);
            }

            Instantiate(particles, transform.position, Quaternion.identity);
 
              Destroy(gameObject);
         
        }
        if (obj.tag == "Wall")
        { 
            Instantiate(particles, transform.position, Quaternion.identity);
            Destroy(gameObject);
           
        }
    } 
}
