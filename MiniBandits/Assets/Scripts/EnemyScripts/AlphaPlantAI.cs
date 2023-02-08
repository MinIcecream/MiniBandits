using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaPlantAI : EnemyAI, IDamageable, IAffectable
{
    public GameObject projectile;

    public override void StartLevel()
    {
        StartCoroutine(FireTimer());
    }
    IEnumerator FireTimer()
    {
        player = GameObject.FindWithTag("Player");
        while (true)
        {
            yield return new WaitForSeconds(0.8f);
            if (player == null)
            {
                break;
            }
            Vector2 dir = ((Vector2)(player.transform.position - transform.position)).normalized;
            for (int i =0; i < 4; i++)
            { 
                //makes projectile
                var newProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
                //shoots projectile at player position
                Vector2 newVector = Quaternion.Euler(0, 0, 90*i) * dir;
                newProjectile.GetComponent<TESTPlayerProjectile>().SetDir(newVector);
                //waits 1 second before shooting another
            } 
        }
    }
    public override void Knockback(float m, Vector2 s)
    {
        return;
    }
}