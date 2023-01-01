using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public new string name;
    public Health health;
    public bool hasDied = false;
    public GameObject player;

    public virtual void StartLevel()
    {

    }

    public virtual void Awake()
    {
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
        transform.localScale = new Vector2(2.2f, 2.2f);

        yield return new WaitForSeconds(0.1f);

        transform.localScale = new Vector2(2f, 2f);
    }
}
