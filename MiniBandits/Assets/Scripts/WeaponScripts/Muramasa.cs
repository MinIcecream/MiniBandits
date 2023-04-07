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
         
         
        newProjectile.GetComponent<BaseProjectile>().SetDir(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        newProjectile.GetComponent<BaseProjectile>().damage = damage; 
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
