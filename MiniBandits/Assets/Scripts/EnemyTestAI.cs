using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTestAI : MonoBehaviour
{
    public GameObject projectile;
    Health health;
    bool hasDied = false;
    GameObject player;
    public int bulletDeathTimer = 2;

    void Awake()
    {
        health = GetComponent<Health>();
        StartCoroutine(FireTimer()); 
        
    }
    void Update()
    {
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
    IEnumerator FireTimer()
    {
        player = GameObject.FindWithTag("Player");
        while (true)
        {
            yield return new WaitForSeconds(1f);
            if (player == null)
            {
                break;
            }
            //makes projectile
            var newProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
            //shoots projectile at player position
            newProjectile.GetComponent<TESTPlayerProjectile>().SetDir(((Vector2)(player.transform.position - transform.position)).normalized);
            //destroys projectile after certain time
            Destroy(newProjectile,bulletDeathTimer);
            //waits 1 second before shooting another
            
        }
    }
}
