using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : WeaponTemplate
{ 

    public override void Attack()
    {
        PlayAttackAnimation();
        var newProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
        newProjectile.GetComponent<BaseProjectile>().damage = damage;
        newProjectile.GetComponent<BaseProjectile>().speed = projectileSpeed;
        newProjectile.GetComponent<BaseProjectile>().range = range;
        newProjectile.GetComponent<BaseProjectile>().knockBack = knockBack;
        newProjectile.GetComponent<BaseProjectile>().SetDir((Vector2)(Camera.main.ScreenToWorldPoint(Input.mousePosition))); 
    } 
}
