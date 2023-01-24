using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeballAI : EnemyAI, IDamageable
{
    public GameObject projectile;
    bool firing;
    public int chaseSpeed;

    enum states
    {
        chasing,
        firing,
        wandering
    }
    states state = states.wandering;

    public override void Awake()
    {
        base.Awake();
        player = GameObject.FindWithTag("Player");
    }
    public override void Update()
    {
        base.Update();

        if(player == null)
        {
            return;
        }
        if (state == states.wandering)
        { 
            if (Vector2.Distance(player.transform.position, transform.position) < 5)
            {
                state = states.firing;
            }
            if (player)
            {
                state = states.chasing;
            }
        } 
        if (state==states.firing && !firing)
        {
            if (!player)
            {
                state = states.wandering;
            }
            if (Vector2.Distance(player.transform.position, transform.position) > 5)
            {
                state = states.chasing;
            } 
        }
        if (state == states.chasing)
        {
            if (!player)
            {
                state = states.wandering;
            }
            if (Vector2.Distance(player.transform.position, transform.position) < 5)
            {
                state = states.firing;
            } 
        }

        if (state == states.wandering)
        {
            return;
        }
        if (state == states.firing)
        {
            if (!firing)
            {
                StartCoroutine(Fire());
            }
        }
        if (state == states.chasing)
        {
            Vector2 dir = player.transform.position - transform.position; 

            transform.position = (Vector2)transform.position + dir.normalized * chaseSpeed * Time.deltaTime;
        }
    }
    IEnumerator Fire()
    { 
        firing = true;

        yield return new WaitForSeconds(0.2f);
        //makes projectile
        if (player)
        {
            var newProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
            //shoots projectile at player position
            newProjectile.GetComponent<TESTPlayerProjectile>().SetDir(((Vector2)(player.transform.position - transform.position)).normalized);

        } 

        yield return new WaitForSeconds(1f);

        firing = false;
    }
}
