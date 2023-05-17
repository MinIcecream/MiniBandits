using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AUG : WeaponTemplate
{ 
    public float timeBetweenShots;
     
    public override void Attack()
    { 
        StartCoroutine(Fire());
    }
    IEnumerator Fire()
    {
        for(int i = 0; i < numProjectiles; i++)
        {
            var newProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
              
            newProjectile.GetComponent<BaseProjectile>().damage = damage;
            newProjectile.GetComponent<BaseProjectile>().speed = projectileSpeed;
            newProjectile.GetComponent<BaseProjectile>().knockBack = knockBack;
            newProjectile.GetComponent<BaseProjectile>().range = range;
            newProjectile.GetComponent<BaseProjectile>().SetDir(Camera.main.ScreenToWorldPoint(Input.mousePosition));

            yield return new WaitForSeconds(timeBetweenShots);
        }
    }
}
