using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public new string name;
    [HideInInspector]
    public Health health;
    [HideInInspector]
    public bool hasDied = false;
    [HideInInspector]
    public GameObject player;

    [HideInInspector] public bool canMove;

    public virtual void StartLevel()
    {

    }

    public virtual void Awake()
    {
        canMove = true;
        health = GetComponent<Health>();  
    }
    public virtual void Update()
    { 
        health = GetComponent<Health>();
        if (hasDied)
        {
            return;
        }
        if (health.GetHealth() <= 0)
        {
            Death();
            hasDied = true;
        }
    }
    void Death()
    {
        Destroy(gameObject);
    }  

    public void Damage(int damage)
    { 
        GetComponent<Health>().DealDamage(damage);
        StartCoroutine(DamageAnimation());
    } 
    IEnumerator DamageAnimation()
    { 
        transform.localScale = new Vector2(1.2f, 1.2f);

        yield return new WaitForSeconds(0.1f);

        transform.localScale = new Vector2(1f, 1f);
    }
    public virtual void Knockback(float magnitude, Vector2 src)
    { 
        Vector2 dashDirection = (Vector2)transform.position - src;
        Vector2 dashDistance = (Vector2)dashDirection.normalized * magnitude;
        GetComponent<Rigidbody2D>().velocity = dashDistance;

        canMove = false;

        Invoke("EnableMovement",0.5f);
    }
    public virtual void EnableMovement()
    {
        canMove = true;
    }
}
