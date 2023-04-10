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

            for(int i = 0; i < 3; i++)
            { 
                //makes projectile
                var newProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
                //shoots projectile at player position
                if (player == null)
                {
                    break;
                }
                newProjectile.GetComponent<BaseProjectile>().SetDir(((Vector2)(player.transform.position)));
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
