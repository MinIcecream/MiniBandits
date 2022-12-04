using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{ 
    public LayerMask mask;
    public float speed;
    public GameObject projectile;

    public bool canMove=true;

    Health health;
    bool hasDied = false;

    public bool inCombat = false;

    Vector2 lastPosition = Vector2.zero;

    bool walkingToRoom=false;

    public float dashMagnitude;

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

        Vector3 walkSpeed = move;
        if (walkSpeed.magnitude > 1)
        {
            walkSpeed = walkSpeed.normalized;
        }
        walkSpeed *= Time.deltaTime;
        transform.position += walkSpeed * speed;

        //DASH
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector2 dashDirection = move.normalized;
            Vector2 dashDistance = (Vector2)move.normalized * dashMagnitude; 
 
            transform.position = hitPoint(dashDirection, dashMagnitude);
        }

        //IF MOUSE NOT OVER UI, ATTACK ON CLICK
        if (EventSystem.current.IsPointerOverGameObject()) return;
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        } 
        //END CAPTUING INPUT 
    }

    public Vector2 hitPoint(Vector2 dir, float distance)
    { 
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, distance, mask);

        //Debug.Log("DIRECTIONS: " + dir + " DISTANCE: " + distance);
        //If you hit something: return the point where you hit
        if (hit.collider != null)
        {
            Debug.Log(hit.point);
            return hit.point;
        }

        //Otherwise, just return the original dash end position
        else
        {
            Debug.Log(dir * distance);
            return dir * distance + (Vector2)transform.position;
        } 
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
