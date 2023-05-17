using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : BaseProjectile
{
    public float AOE;

    public override void OnTriggerEnter2D(Collider2D coll)
    {
        GameObject obj = coll.gameObject;
        if (obj.GetComponent<Health>() != null)
        {
            Destroy(gameObject);
        }
        if (obj.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }

    void Explode()
    {
        // Get an array of all the colliders within the specified radius of this game object
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, AOE);

        // Iterate through the colliders and print their names
        foreach (Collider2D hitCollider in hitColliders)
        {
            GameObject obj = hitCollider.gameObject;
 
            if (obj.tag =="Enemy")
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
        }
        InstantiateParticles();
    }
    void OnDisable()
    { 
        Explode();
    }
}
