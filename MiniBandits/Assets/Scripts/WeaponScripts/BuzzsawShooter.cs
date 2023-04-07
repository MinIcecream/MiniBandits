using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuzzsawShooter : WeaponTemplate
{
    public override void Attack()
    {  
        var newProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
         
        newProjectile.GetComponent<BaseProjectile>().SetDir(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        newProjectile.GetComponent<BaseProjectile>().damage = damage; 
    }
}
