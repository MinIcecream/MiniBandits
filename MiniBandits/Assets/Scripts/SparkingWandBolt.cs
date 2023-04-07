using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparkingWandBolt : BaseProjectile
{
    public float curveSpeed = 1f;
    public float maxTorque = 10f;
    public float forceMagnitude = 10f;

    void FixedUpdate()
    {  
        Transform closestEnemy=null;
        // Find all objects with the given tag
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("Enemy");

        float shortestDistance = Mathf.Infinity;
        foreach (GameObject obj in objectsWithTag)
        {
            // Calculate distance to the current object
            float distance = Vector2.Distance(transform.position, obj.transform.position);

            // If the current object is closer than the previous closest object, update closestObject
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                closestEnemy = obj.transform;
            }
        }

        if (closestEnemy != null)
        {
            Vector2 directionToTarget = (closestEnemy.position - transform.position).normalized;

            // Calculate the angle between the current direction and the target direction
            float angleToTarget = Vector2.SignedAngle(transform.up, directionToTarget);

            // Calculate the torque to apply
            float torque = Mathf.Clamp(angleToTarget * curveSpeed, -maxTorque, maxTorque);

            // Apply torque and force towards the target
            rb.AddTorque(torque);
            rb.AddForce(directionToTarget * forceMagnitude);
        } 
    }  
 
}
