using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AWPProjectile : BaseProjectile
{
    int hitEnemies = 0;

    public override void OnTriggerEnter2D(Collider2D coll)
    {
        GameObject obj = coll.gameObject;

        if (obj.GetComponent<Health>() != null)
        {
            hitEnemies++;
            if (obj.GetComponent<IDamageable>() != null)
            {
                obj.GetComponent<IDamageable>().Damage(damage);
            }
            if (obj.GetComponent<IAffectable>() != null)
            {
                obj.GetComponent<IAffectable>().Knockback(knockBackAmt, transform.position);
            }
            InstantiateParticles();

            if (hitEnemies > 1)
            {
                Destroy(gameObject);
            }
        }
        if (obj.tag == "Wall")
        {
            InstantiateParticles();
            Destroy(gameObject); 
        }
    }
}
