using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigSword : WeaponTemplate
{
    new Collider2D collider;

    public override void Awake()
    {
        base.Awake();
        collider = GetComponent<Collider2D>();
    }
    public override void Attack()
    {
        GetComponent<ParticleSystem>().Play();
        transform.localRotation = Quaternion.Euler(0, 0, -90);
        collider.enabled = true;
        StartCoroutine(DisableCollider());
    }

    IEnumerator DisableCollider()
    { 
        yield return new WaitForSeconds(0.5f); 
        collider.enabled = false;
        transform.localRotation = Quaternion.Euler(0, 0, 0);
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Enemy")
        {
            coll.gameObject.GetComponent<Health>().DealDamage(damage);
        }
    }
}
