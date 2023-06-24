using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipWeapon : MonoBehaviour
{
    private SpriteRenderer sprite;
    GameObject player;

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
        //Get the Screen positions of the object
        Vector2 positionOnScreen = Camera.main.WorldToViewportPoint(player.transform.position);

        //Get the Screen position of the mouse
        Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);

        if (mouseOnScreen.x < positionOnScreen.x)
        {
            sprite.flipY = true;
        }
        else
        {
            sprite.flipY = false;
        }
    }
    public void FlipLeft()
    {
        sprite.flipY = true;
    }
    public void FlipRight()
    {
        sprite.flipY = false;
    }
}
