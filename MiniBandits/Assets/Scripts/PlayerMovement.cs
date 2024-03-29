using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using EZCameraShake;

public class PlayerMovement : MonoBehaviour, IDamageable
{
    public Joystick joystick;
    public LayerMask mask;
    [SerializeField] private TrailRenderer dashTrail;
    [SerializeField] private float dashTime;
    [HideInInspector]
    public bool canMove=true;

    Health health;

    [HideInInspector]
    bool hasDied = false;

    [HideInInspector]
    public bool inCombat = false;

    Vector2 lastPosition = Vector2.zero;
     
    public float dashMagnitude;

    public Animator walkAnim;

    //Slow effect sets this to 5.
    [HideInInspector]
    public float movementSpeed=1;

    void Awake()
    {
        health = GetComponent<Health>();
    }

    void Update()
    { 
       /*
        if (Input.GetKeyDown("j"))
        {
            Debug.Log(RoomOptionGenerator.GenerateRandomArmor().name);
        }
         */
        if (hasDied)
        {
            canMove = false;
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
        if (GetComponent<Rigidbody2D>().velocity.magnitude==0)
        {
            walkAnim.SetBool("Walk", false);
          //  transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(Vector3.zero), 90 * Time.deltaTime);
        }
        else
        {
            walkAnim.SetBool("Walk", true);
        }
 

        //CAPTURING INPUT
        lastPosition = transform.position;

        if (!canMove)
        {
            return;
        }
        Vector2 move = joystick.input;

        Vector2 walkSpeed = move;
        if (walkSpeed.magnitude > 1)
        {
            walkSpeed = walkSpeed.normalized;
        }

        Player stats = GetComponent<Player>(); 

        GetComponent<Rigidbody2D>().velocity = walkSpeed * stats.speed*movementSpeed*0.1f;
 
         
 
        //END CAPTUING INPUT 
    }

    public void Dash(Vector2 dir){
        StartCoroutine(DashDuration(dir));
    }
    public IEnumerator DashDuration(Vector2 move)
    {
        dashTrail.emitting = true;
        GetComponent<PlayerStamina>().UseStamina(1);
        Vector2 dashDirection = move.normalized;
        Vector2 dashDistance = (Vector2)move.normalized * dashMagnitude;
        GetComponent<PlayerHealth>().MakeInvincible(); 
        GetComponent<Rigidbody2D>().AddForce(dashDistance/15);
        //transform.position = hitPoint(dashDirection, dashMagnitude);

        yield return new WaitForSeconds(dashTime);
        GetComponent<Rigidbody2D>().velocity = Vector2.zero; 
        dashTrail.emitting = false;
    }

    //OBSELETE NOW
    public Vector2 hitPoint(Vector2 dir, float distance)
    { 
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, distance, mask);

        //Debug.Log("DIRECTIONS: " + dir + " DISTANCE: " + distance);
        //If you hit something: return the point where you hit
        if (hit.collider != null)
        { 
            return hit.point;
        }

        //Otherwise, just return the original dash end position
        else
        { 
            return dir * distance + (Vector2)transform.position;
        } 
    }

    void Death()
    {
        Destroy(gameObject);
    }  
    public void Damage(int damage)
    {
        CameraShaker.Instance.ShakeOnce(4f, 4f, .1f, .1f);
        GetComponent<PlayerHealth>().DealDamage(damage);
        StartCoroutine(DamageAnimation());
    }
    IEnumerator DamageAnimation()
    {
        transform.localScale = new Vector2(1.2f, 1.2f);

        yield return new WaitForSeconds(0.1f);

        transform.localScale = new Vector2(1f, 1f);
    }
}
