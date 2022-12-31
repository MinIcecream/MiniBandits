using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : WeaponTemplate
{
    public int min, max;

    public override void Attack()
    {
        int bullets = Random.Range(min, max);

        for (int i = 0; i < bullets; i++)
        {
            var newProjectile = Instantiate(projectile, transform.position, Quaternion.identity);

            Vector2 unNormalizedDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            Vector2 dir= (Vector2)(unNormalizedDir.normalized);

            Vector2 rotatedDir = Rotate(dir, Random.Range(-10, 10));
            newProjectile.GetComponent<TESTPlayerProjectile>().SetDir(rotatedDir);
            newProjectile.GetComponent<TESTPlayerProjectile>().damage = damage; 
        } 
    }
    public Vector2 Rotate(Vector2 v, float degrees)
    {
        float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
        float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);

        float tx = v.x;
        float ty = v.y;
        v.x = (cos * tx) - (sin * ty);
        v.y = (sin * tx) + (cos * ty);
        return v;
    }
}
