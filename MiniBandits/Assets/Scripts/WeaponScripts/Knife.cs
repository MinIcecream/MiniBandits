using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : WeaponTemplate
{
    public SpriteRenderer  ren;

    [HideInInspector]
    public bool knifeFired;

    public override void Attack()
    {
        if (!knifeFired)
        {
            PlayAttackAnimation();
            knifeFired = true;

            var newProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
            newProjectile.GetComponent<BaseProjectile>().damage = damage;
            newProjectile.GetComponent<BaseProjectile>().knockBack = knockBack;
            newProjectile.GetComponent<BaseProjectile>().speed = projectileSpeed;
            newProjectile.GetComponent<BaseProjectile>().range = range;
            newProjectile.GetComponent<KnifeProjectile>().parent = this;
            newProjectile.GetComponent<BaseProjectile>().SetDir(attackDir);
        }

    }
    public override void Update()
    {
        base.Update();
        if (knifeFired)
        {
            ren.enabled = false;
        }
        else
        {
            ren.enabled = true;
        }
    }
}
