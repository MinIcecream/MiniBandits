using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour, IDamageable
{ 
    [HideInInspector]
    public Health health;
    [HideInInspector]
    public bool hasDied = false;
    [HideInInspector]
    public GameObject player;
    [HideInInspector]
    public Coroutine attackCoroutine; 
    public float attackCooldown;
    [HideInInspector] public bool canAttack;

    [HideInInspector] public bool canMove;

    float scale;
    public int damage;

    public virtual void Scale(int difficultyLevel)
    {
        health.SetMaxAndCurrentHealth((int)(health.GetMaxHealth()*(0.2f*difficultyLevel+1)));
        damage = (int)( damage* (1 + 0.2f * difficultyLevel));
    }

    public virtual void StartLevel()
    {
        canAttack = true;
        canMove = true;
    }

    public virtual void Awake()
    {
        canMove = true;
        player = GameObject.FindWithTag("Player");
        health = GetComponent<Health>();
        scale = transform.localScale.x;
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
        transform.localScale = new Vector2(scale*1.2f, scale*1.2f);

        yield return new WaitForSeconds(0.1f);

        transform.localScale = new Vector2(scale, scale);
    }
    public virtual void Knockback(float magnitude, Vector2 src)
    { 
        Vector2 dashDirection = (Vector2)transform.position - src;
        Vector2 dashDistance = (Vector2)dashDirection.normalized * magnitude;
        GetComponent<Rigidbody2D>().velocity = dashDistance;

        canMove = false;

        Invoke("EnableMovement", 0.5f);
        StartCoroutine(Stagger());
    }
    public virtual void EnableMovement()
    {
        canMove = true;
    }
    public virtual IEnumerator Stagger()
    {
        if (attackCoroutine == null)
        {
            yield break;
        }
        StopCoroutine(attackCoroutine); 
        canAttack = true;
    }
}
