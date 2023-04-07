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
             
            newProjectile.GetComponent<BaseProjectile>().SetDir(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            newProjectile.GetComponent<BaseProjectile>().damage = damage;

            yield return new WaitForSeconds(timeBetweenShots);
        }
    }
}
