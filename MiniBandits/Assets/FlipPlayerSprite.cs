using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipPlayerSprite : MonoBehaviour
{
    public string direction = "right";
    SpriteRenderer sprite;

    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if (Input.GetAxis("Horizontal") < 0)
        {
            direction = "left";
        }
        else if (Input.GetAxis("Horizontal") > 0)
        {
            direction = "right";
        }
        /*
        if (direction == "left")
        {
            transform.eulerAngles = new Vector3(
            transform.eulerAngles.x,
            transform.eulerAngles.y + 180,
            transform.eulerAngles.z
            ); 
        }
        else
        {
            transform.eulerAngles = new Vector3(
            transform.eulerAngles.x,
            transform.eulerAngles.y + 180,
            transform.eulerAngles.z
            );
        }*/
    }
}
