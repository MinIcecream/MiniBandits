using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTPlayerProjectile : MonoBehaviour
{
    Vector2 direction;
    public float speed;
    [HideInInspector]
    public int damage;
    public GameObject particles;
    public bool destroyOnHit=true;
    public bool destroyOnWall = true;
    public new Collider2D collider;
    public float knockBackAmt;

    void Awake()
    {
        if (GetComponent<Collider2D>() != null)
        { 
            collider = GetComponent<Collider2D>();
        } 
        collider.enabled = false;
    }
    public void SetDir(Vector2 dir)
    {
        direction = dir;
        StartCoroutine(EnableCollider());
        GetComponent<Rigidbody2D>().velocity = (Vector2)direction * speed; 
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

            Instantiate(particles, transform.position, Quaternion.identity);

            if (destroyOnHit)
            { 
                Destroy(gameObject);
            } 
        }
        if (obj.tag == "Wall")
        {
            if (destroyOnWall)
            { 
                Instantiate(particles, transform.position, Quaternion.identity);
                Destroy(gameObject);
            } 
        }
    }
}
