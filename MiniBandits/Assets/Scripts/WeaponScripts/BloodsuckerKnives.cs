using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodsuckerKnives : WeaponTemplate
{
    public int angleRange;

    public override void Attack()
    { 
        float distance = Vector2.Distance(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));

        Vector2 targetPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Get the direction vector pointing towards the target point
        Vector2 direction = targetPoint - (Vector2)transform.position;

        // Generate 6 random points within the given distance and angle range
        for (int i = 0; i < numProjectiles; i++)
        {
            float randomAngle = Random.Range(-angleRange, angleRange);
            Vector2 rotatedDirection = Quaternion.Euler(0f, 0f, randomAngle) * direction.normalized;
            Vector2 randomPoint = (Vector2)transform.position + rotatedDirection * distance;

            var newProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
            newProjectile.GetComponent<BaseProjectile>().damage = damage;
            newProjectile.GetComponent<BaseProjectile>().speed = projectileSpeed;
            newProjectile.GetComponent<BaseProjectile>().range = range;
            newProjectile.GetComponent<BaseProjectile>().knockBack = knockBack;

            newProjectile.GetComponent<BaseProjectile>().SetDir(randomPoint);
        }
    }
}
