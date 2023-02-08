using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyBookProjectile : MonoBehaviour
{
    [HideInInspector]
    public int damage;
    public float knockBackAmt;

    void Update()
    {
        transform.eulerAngles = Vector3.zero;
    }
    public virtual void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.GetComponent<Health>() != null)
        {
            coll.gameObject.GetComponent<IDamageable>().Damage(damage);
        }
        if (coll.gameObject.GetComponent<IAffectable>() != null)
        {
            coll.gameObject.GetComponent<IAffectable>().Knockback(knockBackAmt, transform.position);
        }
    }
}
