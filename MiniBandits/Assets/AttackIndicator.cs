using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
 

public class AttackIndicator : MonoBehaviour
{
    public enum shapes
    {
        line,
        circle
    }
    public shapes shape;

    //CIRCLE
    [SerializeField] private float radius;

    //LINE
    [SerializeField] private float width;

    //Generaate a circle indicator
    public void GenerateAttackIndicator(Vector2 pos)
    {
        var newIndicator = Instantiate(Resources.Load<GameObject>("Misc/CircleIndicator"), pos, Quaternion.identity);
        newIndicator.transform.localScale = new Vector2(radius * 2, radius * 2);
    }

    //Line indicator
    public void GenerateAttackIndicator(Vector2 origin, Vector2 destination)
    {
        Vector2 pointInBetween = Vector2.Lerp(origin, destination, 0.5f);

        var newIndicator = Instantiate(Resources.Load<GameObject>("Misc/LineIndicator"), pointInBetween, Quaternion.identity);

        Vector2 direction = destination - origin;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        float length = Vector2.Distance(origin, destination);

        newIndicator.transform.localScale = new Vector2(length*1.5f, width);

        newIndicator.transform.eulerAngles = new Vector3(0f, 0f, angle);
    }         
} 