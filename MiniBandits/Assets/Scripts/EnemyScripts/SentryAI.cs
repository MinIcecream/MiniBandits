using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentryAI : EnemyAI, IAffectable
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
            yield return new WaitForSeconds(1.3f);
            if (player == null)
            {
                break;
            }

            for(int i = 0; i < 3; i++)
            { 
                //makes projectile
                var newProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
                //shoots projectile at player position
                newProjectile.GetComponent<TESTPlayerProjectile>().SetDir(((Vector2)(player.transform.position - transform.position)).normalized);
                //waits 1 second before shooting another
                yield return new WaitForSeconds(0.2f);
            }

        }
    }

    public override void Knockback(float m, Vector2 s)
    {
        return;
    }
}
