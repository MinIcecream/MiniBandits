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
        //resources.load circle
    }

    //Line indicator
    public void GenerateAttackIndicator(Vector2 origin, Vector2 destination)
    {
        //resources.load line
    } 
} 