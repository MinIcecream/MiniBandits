using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShimmeringRingParent : WeaponTemplate
{
    public override void Attack()
    {
        var newProjectile = Instantiate(projectile, transform.position, Quaternion.identity);

        newProjectile.GetComponent<BaseProjectile>().damage = damage;
        newProjectile.GetComponent<BaseProjectile>().speed = projectileSpeed;
        newProjectile.GetComponent<BaseProjectile>().knockBack = knockBack;
        newProjectile.GetComponent<BaseProjectile>().range = range;
        newProjectile.GetComponent<BaseProjectile>().SetDir(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }
}
