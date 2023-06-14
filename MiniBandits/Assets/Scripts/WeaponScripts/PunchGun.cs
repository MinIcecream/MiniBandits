using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchGun : WeaponTemplate
{
    public Collider2D coll;
    public GameObject glove;

    [HideInInspector]
    public bool gloveFired;

    public override void Attack()
    {
        if (!gloveFired)
        {
            PlayAttackAnimation();
            gloveFired = true;

            var newProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
             
            newProjectile.GetComponent<BoxingGlove>().parent = this;
            newProjectile.GetComponent<BaseProjectile>().damage = damage;
            newProjectile.GetComponent<BaseProjectile>().knockBack = knockBack;
            newProjectile.GetComponent<BaseProjectile>().speed = projectileSpeed;
            newProjectile.GetComponent<BaseProjectile>().range = range; 
            newProjectile.GetComponent<BaseProjectile>().SetDir(attackDir); 
        } 

    }   
    public override void Update()
    {
        base.Update();
        if (gloveFired)
        {
            glove.SetActive(false);
        }
        else
        {
            glove.SetActive(true);
        }
    }
}
