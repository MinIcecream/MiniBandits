using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissile : BaseProjectile
{
    public float rotateSpeed = 200f;
    Transform player;

    public override void Awake(){
        base.Awake();
        player=GameObject.FindWithTag("Player").transform;
    }
    void FixedUpdate()
    {
        if (player!=null)
        {
            Vector2 direction = (Vector2)player.position-(Vector2)transform.position;

            direction.Normalize();

            float rotateAmount = Vector3.Cross(direction, transform.up).z;

            rb.angularVelocity = -rotateAmount * rotateSpeed;

            rb.velocity = transform.up * speed;
        }
    }
}
