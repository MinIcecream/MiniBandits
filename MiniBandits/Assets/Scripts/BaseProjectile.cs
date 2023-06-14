using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseProjectile : MonoBehaviour
{    
    public GameObject particles;

    public bool destroyOnHit = true;
    public bool destroyOnWall = true;

    public new Collider2D collider;
      
    //Set through script
    [HideInInspector]
    public int damage;
    [HideInInspector]
    public int knockBack = 3; 

    [HideInInspector]
    public int range = 1000;
    [HideInInspector]
    public float speed;

    Vector2 origin;

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
    public virtual void FixedUpdate()
    {
        if(Vector2.Distance(transform.position, origin) > range)
        {
            Destroy(gameObject);
        }
    }
    public virtual void SetDir(Vector2 targetPt)
    {
        if (gameObject.tag == "EnemyProjectile")
        {
            origin = transform.position;
        }
        else
        { 
            origin = GameObject.FindWithTag("Player").transform.position;
        } 
        targetPoint = targetPt; 
        Vector2 direction = (targetPt - (Vector2)origin).normalized;
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
                obj.GetComponent<IAffectable>().Knockback(knockBack, transform.position);
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

    public void Destroy(GameObject obj)
    {
        if (collider != null)
        { 
            collider.enabled = false;
        } 
        Object.Destroy(obj);
    }
}
