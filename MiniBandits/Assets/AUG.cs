using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AUG : WeaponTemplate
{
    public int bulletsPerBurst;
    public float timeBetweenShots;


    public override void Attack()
    { 
        StartCoroutine(Fire());
    }
    IEnumerator Fire()
    {
        for(int i = 0; i < bulletsPerBurst; i++)
        {
            var newProjectile = Instantiate(projectile, transform.position, Quaternion.identity);

            Vector2 unNormalizedDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            Vector2 dir = (Vector2)(unNormalizedDir.normalized);
             
            newProjectile.GetComponent<TESTPlayerProjectile>().SetDir(dir);
            newProjectile.GetComponent<TESTPlayerProjectile>().damage = damage;

            yield return new WaitForSeconds(timeBetweenShots);
        }
    }
}
