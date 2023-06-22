using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipWeapon : MonoBehaviour
{
    private SpriteRenderer sprite;
    GameObject player;
    Joystick joystick;

    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        player = GameObject.FindWithTag("Player"); 
    }
    void FixedUpdate()
    {
        if (player == null)
        {
            return;
        }
        Flip();
    }
    public void Flip()
    { 
        Vector2 mouseOnScreen = (Vector2)player.GetComponent<GetClosestEnemyPosition>().GetClosestEnemyPos();

        if (mouseOnScreen == Vector2.zero)
        {
            return;
        }

        if (mouseOnScreen.x < player.transform.position.x)
        {
            sprite.flipY = true; 
        }
        else
        {
            sprite.flipY = false; 
        }
    } 
}
