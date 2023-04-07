using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodsuckerKnife : BaseProjectile
{
    public float healAmt;
    public override void OnTriggerEnter2D(Collider2D coll)
    {
        GameObject obj = coll.gameObject;
        if (obj.GetComponent<Health>() != null)
        {
            if (obj.GetComponent<IDamageable>() != null)
            {
                obj.GetComponent<IDamageable>().Damage(damage);
                int healAmount = Mathf.CeilToInt(damage * healAmt);

                if (GameObject.FindWithTag("Player") != null)
                { 
                    GameObject.FindWithTag("Player").GetComponent<Health>().Heal(healAmount);
                } 
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
}
