using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigSword : WeaponTemplate
{
    new Collider2D collider;
    public float knockBackAmt;

    public override void Start()
    {
        base.Start();
        collider = GetComponent<Collider2D>();
    }
    public override void Attack()
    {
        GetComponent<ParticleSystem>().Play();
        transform.localRotation = Quaternion.Euler(0, 0, 90);
        collider.enabled = true;
        StartCoroutine(DisableCollider());
    }

    IEnumerator DisableCollider()
    {
        yield return new WaitForSeconds(0.1f);
        collider.enabled = false;
        yield return new WaitForSeconds(0.4f);
        GetComponent<ParticleSystem>().Stop(); 
        transform.localRotation = Quaternion.Euler(0, 0, 0);
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        GameObject obj = coll.gameObject;
        if (obj.tag == "Enemy")
        {
            obj.GetComponent<IDamageable>().Damage(damage);
        }
        if (obj.GetComponent<IAffectable>() != null)
        {
            obj.GetComponent<IAffectable>().Knockback(knockBackAmt, transform.position);
        }
    }
}
