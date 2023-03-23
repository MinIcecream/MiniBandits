using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidSpit : MonoBehaviour
{
    public float speed = 10f;
    public Vector2 targetPoint;
    public float hitThreshold = 0.1f;
    public GameObject acidPool;
    private Rigidbody2D rb; 

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();  
    }
    public void SetTarget(Vector2 pt)
    {
        targetPoint = pt;
        Vector2 dir = (pt - (Vector2)transform.position).normalized;
        rb.velocity = (Vector2)dir * speed;
    }
    void Update()
    {
        transform.up = targetPoint-(Vector2)transform.position;
        if (Vector2.Distance(transform.position, targetPoint) < hitThreshold)
        { 
            Instantiate(acidPool, transform.position, Quaternion.identity);
            // Destroy the projectile
            Destroy(gameObject);
        }
    }
     
}
