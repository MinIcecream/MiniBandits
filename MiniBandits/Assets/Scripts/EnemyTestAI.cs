using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTestAI : MonoBehaviour
{
    public GameObject projectile;
    Health health;
    bool hasDied = false;
    GameObject player;

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
            if (player == null)
            {
                break;
            }
            var newProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
            newProjectile.GetComponent<TESTPlayerProjectile>().SetDir(((Vector2)(player.transform.position - transform.position)).normalized);
            yield return new WaitForSeconds(1f);
        }
    }
}
