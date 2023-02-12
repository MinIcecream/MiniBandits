using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Muramasa : WeaponTemplate
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
        var newProjectile = Instantiate(projectile, transform.position, Quaternion.identity);

        Vector2 unNormalizedDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        Vector2 dir = (Vector2)(unNormalizedDir.normalized);
         
        newProjectile.GetComponent<TESTPlayerProjectile>().SetDir(dir);
        newProjectile.GetComponent<TESTPlayerProjectile>().damage = damage;

        newProjectile.transform.right = dir;
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
    }
}
