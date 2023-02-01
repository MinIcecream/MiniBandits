using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilMage : EnemyAI, IDamageable
{
    public GameObject projectile;
    bool firing;
    public int chaseSpeed;

    bool canAttack = false;
    string lastAttack = "circle";
    bool currentlyAttacking = false;

    public override void Awake()
    {
        base.Awake();
        player = GameObject.FindWithTag("Player");
        StartCoroutine(AttackCooldown());
    }
    public override void Update()
    { 
        base.Update();

        if (player == null)
        {
            return;
        }
        if (!currentlyAttacking)
        {
            transform.position += (player.transform.position- transform.position).normalized * Time.deltaTime * 2;
        } 
        if (canAttack)
        {
            currentlyAttacking = true;
            if (lastAttack == "circle")
            {
                lastAttack = "line";
                canAttack = false;
                StartCoroutine(SpawnCircles()); 
            }
            else if (lastAttack == "line")
            {
                lastAttack = "rapid";
                canAttack = false;
                StartCoroutine(SpawnLines());
            }
            else if (lastAttack == "rapid")
            {
                lastAttack = "circle";
                canAttack = false;
                StartCoroutine(RapidFire());
            }
        }
    } 

    IEnumerator AttackCooldown()
    {
        currentlyAttacking = false;
        yield return new WaitForSeconds(Random.Range(5, 8));
        canAttack = true;
    }
    //Shoot a bunch of projectiles at you
    //Shoot a bunch in a circle with gaps
    //Shoot in a line that fills the screen
    IEnumerator SpawnLines()
    {
        yield return new WaitForSeconds(1.5f);
        for(int j = 0; j < 3; j++)
        { 
            if (j == 0||j==2)
            {
                if (player == null)
                {
                    break;
                }
                for (int i = 0; i < 50; i++)
                {
                    var newProjectile = Instantiate(projectile, new Vector2(transform.position.x - 30 + 1.8f * i, transform.position.y + 20), Quaternion.identity);
                    //shoots projectile at player position
                    newProjectile.GetComponent<TESTPlayerProjectile>().SetDir(new Vector2(0, -1));
                    newProjectile.GetComponent<TESTPlayerProjectile>().destroyOnWall = false;
                }
            }
            else
            { 
                for (int i = 0; i < 50; i++)
                {
                    if (player == null)
                    {
                        break;
                    }
                    var newProjectile = Instantiate(projectile, new Vector2(transform.position.x - 20, transform.position.y + -40+1.8f*i), Quaternion.identity);
                    //shoots projectile at player position
                    newProjectile.GetComponent<TESTPlayerProjectile>().SetDir(new Vector2(1, 0));
                    newProjectile.GetComponent<TESTPlayerProjectile>().destroyOnWall = false;
                }
            }
            yield return new WaitForSeconds(3f);
        }
        StartCoroutine(AttackCooldown());
    }
    IEnumerator RapidFire()
    {
        yield return new WaitForSeconds(1.5f);
        for (int i =0; i < 10; i++)
        {
            if (player == null)
            {
                break;
            }
            var newProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
            //shoots projectile at player position
            newProjectile.GetComponent<TESTPlayerProjectile>().SetDir(((Vector2)(player.transform.position - transform.position)).normalized);
            yield return new WaitForSeconds(0.2f);
        }
        StartCoroutine(AttackCooldown());
    }
    IEnumerator SpawnCircles()
    {
        yield return new WaitForSeconds(1.5f);
        for (int i = 0; i < 3; i++)
        {
            if (player == null)
            {
                break;
            }
            int numProjectiles=30;
            int gap = 6;
            //spawn
            for (int j = 0; j < numProjectiles; j++)
            {
                if (j % gap != 0)
                { 
                    float ang = (360 / numProjectiles) * j;

                    var newProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
                    Vector2 dir = (Vector2)(Quaternion.Euler(0, 0, ang) * Vector2.right); 
                    newProjectile.GetComponent<TESTPlayerProjectile>().SetDir(dir.normalized);
                } 
            }
            yield return new WaitForSeconds(2f);
        }
        StartCoroutine(AttackCooldown());
    } 
}
