using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetClosestEnemyPosition : MonoBehaviour
{
    public Joystick joystick;
    public Vector2 posToReturn;

    public Vector2 GetClosestEnemyPos()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
         

        if (joystick.input != Vector2.zero)
        {
            posToReturn = joystick.input;
        }
        Vector2 closestEnemyPosition = posToReturn+ (Vector2)transform.position;
         
        
        float closestDistance = Mathf.Infinity;

        
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);

            if (distanceToEnemy < closestDistance)
            {
                closestDistance = distanceToEnemy;
                closestEnemyPosition = enemy.transform.position;
            }
        }
     //   Debug.Log("Direction: " +(closestEnemyPosition- (Vector2)transform.position) +  "  Player Position: "+ transform.position + "  Target position: "+ closestEnemyPosition);
        return closestEnemyPosition;
    }
}
