using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuzzsawShooter : WeaponTemplate
{
    public override void Attack()
    {  
        var newProjectile = Instantiate(projectile, transform.position, Quaternion.identity);

        Vector2 unNormalizedDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        Vector2 dir = (Vector2)(unNormalizedDir.normalized);
         
        newProjectile.GetComponent<TESTPlayerProjectile>().SetDir(dir);
        newProjectile.GetComponent<TESTPlayerProjectile>().damage = damage; 
    }
}
