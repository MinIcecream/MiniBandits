using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeadPipe : WeaponTemplate
{
    Animator anim;
    public int knockBackAmt;
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
            coll.gameObject.GetComponent<IAffectable>().Knockback(knockBackAmt, transform.position);
        }
    }
}
