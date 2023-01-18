using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTPlayerProjectile : MonoBehaviour
{
    Vector2 direction;
    public float speed;
    public int damage;
    public GameObject particles;

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
            coll.gameObject.GetComponent<IDamageable>().Damage(damage);
            Instantiate(particles, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        if (coll.gameObject.tag == "Wall")
        {
            Instantiate(particles, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
