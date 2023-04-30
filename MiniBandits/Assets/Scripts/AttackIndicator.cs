using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
 

public class AttackIndicator : MonoBehaviour
{
    LayerMask wallMask;

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

    void Start()
    {
        wallMask = LayerMask.GetMask("Terrain");
    }
    //Generaate a circle indicator at point, or line going in difrection until hit wall
    public void GenerateAttackIndicator(Vector2 pos)
    {
        if (shape == shapes.circle)
        { 
            var newIndicator = Instantiate(Resources.Load<GameObject>("Misc/CircleIndicator"), pos, Quaternion.identity);
            newIndicator.transform.localScale = new Vector2(radius * 2, radius * 2);
        }
        else
        {  
            RaycastHit2D hitWall = Physics2D.Raycast(transform.position, pos, 30, wallMask);

            GenerateAttackIndicator(transform.position, hitWall.point);

        }
    }

    //Line indicator with start and end points
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