using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTPlayerProjectile : MonoBehaviour
{
    Vector2 direction;
    public float speed;
    public int damage;

    public void SetDir(Vector2 dir)
    {
        direction = dir;
    }

    void Update()
    {
        transform.position = (Vector2)transform.position+direction * speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.GetComponent<Health>() != null)
        {
            coll.gameObject.GetComponent<Health>().DealDamage(damage);
        }
        if (coll.gameObject.tag == "Wall")
        { 
            Destroy(gameObject);
        }
    }
}
