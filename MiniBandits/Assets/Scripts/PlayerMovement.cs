using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public GameObject projectile;


    Health health;
    bool hasDied = false;

    Vector2 lastPosition = Vector2.zero;

    void Awake()
    {
        health = GetComponent<Health>();
    }

    void Update()
    {
        if (hasDied)
        {
            return;
        }

        //CAPTURING INPUT
        lastPosition = transform.position;

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f);

        if (move.magnitude > 1)
        {
            move = move.normalized;
        }
        move *= Time.deltaTime;
        transform.position += move * speed; 

        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }

        if ((Vector2)transform.position == lastPosition)
        {
            GetComponent<Animation>().enabled = false;
            transform.rotation = Quaternion.RotateTowards(transform.rotation,Quaternion.Euler(Vector3.zero),90*Time.deltaTime);
        }
        else
        { 
            GetComponent<Animation>().enabled = true;
        }
        //END CAPTUING INPUT


        //CHECKING IF PLAYTER DIED 
        if (health.GetHealth() <= 0)
        {
            Death();
            hasDied = true;
        }
        //END CHECKING IF PLOAYRWE DEID
    }

    void Death()
    {
        Destroy(gameObject);
    }
    void Attack()
    {
        var newProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
        newProjectile.GetComponent<TESTPlayerProjectile>().SetDir(((Vector2)(Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position)).normalized);
    } 
}
