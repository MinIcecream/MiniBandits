using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public GameObject projectile;

    public bool canMove=true;

    Health health;
    bool hasDied = false;

    public bool inCombat = false;

    Vector2 lastPosition = Vector2.zero;

    bool walkingToRoom=false;

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

        //CHECKING IF PLAYTER DIED 
        if (health.GetHealth() <= 0)
        {
            Death();
            hasDied = true;
        }
        //END CHECKING IF PLOAYRWE DEID


        //CHECK IF MOVING. IF SO, PLAY ANIMATION
        if ((Vector2)transform.position == lastPosition)
        {
            GetComponent<Animation>().enabled = false;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(Vector3.zero), 90 * Time.deltaTime);
        }
        else
        {
            GetComponent<Animation>().enabled = true;
        }

        if (walkingToRoom)
        {
            transform.position = (Vector2)transform.position+ new Vector2(0,1) * Time.deltaTime * speed;
            return;
        } 

        //CAPTURING INPUT
        lastPosition = transform.position;

        if (!canMove)
        {
            return;
        }
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f);

        if (move.magnitude > 1)
        {
            move = move.normalized;
        }
        move *= Time.deltaTime;
        transform.position += move * speed;

        if (EventSystem.current.IsPointerOverGameObject()) return;
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        } 
        //END CAPTUING INPUT

         
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
    public void WalkToRoom()
    {
        walkingToRoom = true;
    }
    public void ReachedRoom()
    {
        walkingToRoom = false;
    }
}
