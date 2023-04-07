using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : WeaponTemplate
{
    public int max, min;
    public int angleRange;

    public override void Attack()
    {
        int bullets = Random.Range(min, max);
        float distance = Vector2.Distance(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
      
        Vector2 targetPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Get the direction vector pointing towards the target point
        Vector2 direction = targetPoint - (Vector2)transform.position;

        // Generate 6 random points within the given distance and angle range
        for (int i = 0; i < bullets; i++)
        {
            float randomAngle = Random.Range(-angleRange, angleRange);
            Vector2 rotatedDirection = Quaternion.Euler(0f, 0f, randomAngle) * direction.normalized;
            Vector2 randomPoint = (Vector2)transform.position + rotatedDirection * distance;

            var newProjectile = Instantiate(projectile, transform.position, Quaternion.identity);

            newProjectile.GetComponent<BaseProjectile>().SetDir(randomPoint);
            newProjectile.GetComponent<BaseProjectile>().damage = damage;
        }
    }  
}
