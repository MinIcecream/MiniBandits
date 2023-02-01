using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTPlayerProjectile : MonoBehaviour
{
    Vector2 direction;
    public float speed;
    public int damage;
    public GameObject particles;
    public bool destroyOnHit=true;
    public bool destroyOnWall = true;
    public new Collider2D collider;

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
        if (coll.gameObject.GetComponent<Health>() != null)
        {
            coll.gameObject.GetComponent<IDamageable>().Damage(damage);
            Instantiate(particles, transform.position, Quaternion.identity);
            if (destroyOnHit)
            { 
                Destroy(gameObject);
            } 
        }
        if (coll.gameObject.tag == "Wall")
        {
            if (destroyOnWall)
            { 
                Instantiate(particles, transform.position, Quaternion.identity);
                Destroy(gameObject);
            } 
        }
    }
}
