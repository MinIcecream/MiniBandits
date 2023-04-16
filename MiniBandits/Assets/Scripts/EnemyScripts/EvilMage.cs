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
        for(int j = 0; j < 4; j++)
        { 
            if (j == 0||j==2)
            {
                if (player == null)
                {
                    break;
                }
                for (int i = 0; i < 30; i++)
                {
                    var newProjectile = Instantiate(projectile, new Vector2(transform.position.x - 30 + 1.8f * i, transform.position.y + 20), Quaternion.identity);
                    //shoots projectile at player position
                    newProjectile.GetComponent<BaseProjectile>().damage = damage;
                    newProjectile.GetComponent<BaseProjectile>().SetDir(new Vector2(0, -1)+(Vector2)transform.position);
                    newProjectile.GetComponent<BaseProjectile>().destroyOnWall = false;
                }
            }
            else
            { 
                for (int i = 0; i < 30; i++)
                {
                    if (player == null)
                    {
                        break;
                    }
                    var newProjectile = Instantiate(projectile, new Vector2(transform.position.x - 20, transform.position.y + -40+1.8f*i), Quaternion.identity);
                    //shoots projectile at player position
                    newProjectile.GetComponent<BaseProjectile>().damage = damage;
                    newProjectile.GetComponent<BaseProjectile>().SetDir(new Vector2(1, 0)+(Vector2)transform.position);
                    newProjectile.GetComponent<BaseProjectile>().destroyOnWall = false;
                }
            }
            yield return new WaitForSeconds(2f);
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
            newProjectile.GetComponent<BaseProjectile>().damage = damage;
            newProjectile.GetComponent<BaseProjectile>().SetDir((Vector2)(player.transform.position));
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
            int numProjectiles=50;
            int gap = 2;
            //spawn
            for (int j = 0; j < numProjectiles; j++)
            {
                if (j % gap != 0)
                { 
                    float ang = (360 / numProjectiles) * j;

                    var newProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
                    Vector2 dir = (Vector2)(Quaternion.Euler(0, 0, ang) * Vector2.right);
                    newProjectile.GetComponent<BaseProjectile>().damage = damage;
                    newProjectile.GetComponent<BaseProjectile>().SetDir(dir.normalized+(Vector2)transform.position);
                } 
            }
            yield return new WaitForSeconds(2f);
        }
        StartCoroutine(AttackCooldown());
    } 
    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            coll.gameObject.GetComponent<Health>().DealDamage(80);
        }
    }
}
