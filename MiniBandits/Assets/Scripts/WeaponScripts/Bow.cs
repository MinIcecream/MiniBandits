using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : WeaponTemplate
{
    public float speed;

    public override void Attack()
    {
        var newProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
        newProjectile.GetComponent<BaseProjectile>().damage = damage;
        newProjectile.GetComponent<BaseProjectile>().speed = speed;
        newProjectile.GetComponent<BaseProjectile>().SetDir((Vector2)(Camera.main.ScreenToWorldPoint(Input.mousePosition))); 
    } 
}
