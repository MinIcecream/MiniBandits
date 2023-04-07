using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidSpit : BaseProjectile
{ 
    public float hitThreshold = 0.1f;
    public GameObject acidPool;  

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
