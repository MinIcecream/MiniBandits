using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeadPipe : WeaponTemplate
{
    Animator anim; 
    public override void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
    }
    public override void Attack()
    {
        GetComponent<Collider2D>().enabled = true;
        StartCoroutine(DisableCollider());

        anim.SetTrigger("Attack");
        GetComponent<BoxCollider2D>().size = new Vector2(AOE, GetComponent<BoxCollider2D>().size.y);
    }
    IEnumerator DisableCollider()
    {
        yield return new WaitForSeconds(0.1f);
        GetComponent<Collider2D>().enabled = false;
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.GetComponent<Health>() != null)
        {
            coll.gameObject.GetComponent<IDamageable>().Damage(damage);
        }
        if (coll.gameObject.GetComponent<IAffectable>() != null)
        {
            coll.gameObject.GetComponent<IAffectable>().Knockback(knockBack, transform.position);
        }
    }
}
