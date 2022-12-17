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
}
