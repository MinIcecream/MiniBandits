using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseProjectile : MonoBehaviour
{   
    //Set in inspector
    public float speed;

    public GameObject particles;

    public bool destroyOnHit = true;
    public bool destroyOnWall = true;

    public new Collider2D collider;

    public float knockBackAmt;

    //Set through script
    [HideInInspector]
    public int damage;

    [HideInInspector]
    public Vector2 targetPoint;

    [HideInInspector]
    public Rigidbody2D rb;

    public virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); 

        if (collider != null)
        { 
            collider.enabled = false; 
            StartCoroutine(EnableCollider());
        }
    }
    public void SetDir(Vector2 targetPt)
    {
        targetPoint = targetPt;
        Vector2 direction = (targetPt - (Vector2)transform.position).normalized;
        rb.velocity = (Vector2)direction * speed;
        transform.right = direction;
    }

    IEnumerator EnableCollider()
    {
        yield return new WaitForSeconds(0.08f);
        collider.enabled = true;
    }

    public virtual void OnTriggerEnter2D(Collider2D coll)
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
            InstantiateParticles();

            if (destroyOnHit)
            {
                Destroy(gameObject);
            }
        }
        if (obj.tag == "Wall")
        {
            if (destroyOnWall)
            {
                InstantiateParticles();
                Destroy(gameObject);
            }
        }
    }

    public void InstantiateParticles()
    {
        if (particles == null)
        {
            return;
        } 
        Instantiate(particles, transform.position, Quaternion.identity);
    }
}
