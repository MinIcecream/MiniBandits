using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipPlayerSprite : MonoBehaviour
{
    public string direction = "right";
    SpriteRenderer sprite;
    public Joystick joystick;

    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if(joystick.input == Vector2.zero)
        {
            return;
        } 
        if (joystick.input.x < 0)
        {
            direction = "left";
        }
        else
        {
            direction = "right";
        }
        
        if (direction == "left")
        {
            transform.eulerAngles = new Vector3(
            transform.eulerAngles.x,
            0,
            transform.eulerAngles.z
            ); 
        }
        else
        {
            transform.eulerAngles = new Vector3(
            transform.eulerAngles.x,
            180,
            transform.eulerAngles.z
            );
        }
    }
}
