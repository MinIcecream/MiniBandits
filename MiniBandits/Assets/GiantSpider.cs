using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantSpider : EnemyAI, IDamageable
{
    public LayerMask wallLayer;

    public GameObject cacoon,damageIndicator;

    public int chaseSpeed;

    bool canAttack = false;
    string lastAttack = "spawn";
    bool currentlyAttacking = false;
    LineRenderer lineRen;

    public GameObject landParticles;
    
    public override void Awake()
    {
        base.Awake();
        player = GameObject.FindWithTag("Player");
        StartCoroutine(AttackCooldown());
        lineRen = GetComponent<LineRenderer>();
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
            transform.position += (player.transform.position - transform.position).normalized * Time.deltaTime * chaseSpeed;
        }
        if (canAttack)
        {
            currentlyAttacking = true;
            if (lastAttack == "spawn")
            {
                lastAttack = "ascend";
                canAttack = false;
                StartCoroutine(Ascend());
            }
            else if (lastAttack == "ascend")
            {
                lastAttack = "spawn";
                canAttack = false;
                StartCoroutine(Spawn());
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
    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(1.5f);
         
        float xMin = 0;
        float xMax = 0;
        float yMin = 0;
        float yMax = 0;

        RaycastHit2D right = Physics2D.Raycast(transform.position, Vector2.right, 30, wallLayer);
        if (right.collider != null)
        {
            xMax = right.point.x-2;
        }
        RaycastHit2D left = Physics2D.Raycast(transform.position, Vector2.left, 30, wallLayer);
        if (left.collider != null)
        {
            xMin = left.point.x+2;
        }
        RaycastHit2D up = Physics2D.Raycast(transform.position, Vector2.up, 30, wallLayer);
        if (up.collider != null)
        {
            yMax = up.point.y-2;
        }
        RaycastHit2D down = Physics2D.Raycast(transform.position, Vector2.down, 30, wallLayer);
        if (down.collider != null)
        {
            yMin = down.point.y+2;
        }
         
        for(int i =0; i<5; i++)
        {
            Vector2 newPos = new Vector2(Random.Range(xMin, xMax), Random.Range(yMin, yMax));

            while (Vector2.Distance(transform.position, newPos) > 0.2f)
            { 
                transform.position += ((Vector3)newPos - transform.position).normalized * Time.deltaTime * chaseSpeed*2.5f;
                yield return null;
            }
            yield return new WaitForSeconds(1);
            Instantiate(cacoon, transform.position, Quaternion.identity);
        }
        StartCoroutine(AttackCooldown());
    }
    IEnumerator Ascend()
    { 
        yield return new WaitForSeconds(1.5f);

        lineRen.enabled = true;
        GetComponent<Collider2D>().enabled = false;
        lineRen.SetPosition(0, transform.position);
        lineRen.SetPosition(1, new Vector2(transform.position.x, transform.position.y + 50));
        float targetY = transform.position.y + 50;

        while (transform.position.y < targetY)
        {
            transform.position += Vector3.up * Time.deltaTime * 30;
            lineRen.SetPosition(0, transform.position);
            yield return null; 
        }
        if (player == null)
        {
            yield break;
        }
        yield return new WaitForSeconds(0.5f);
        Vector2 target = player.transform.position;
        Instantiate(damageIndicator, target, Quaternion.identity);

        yield return new WaitForSeconds(0.5f);

        transform.position = new Vector3(target.x, target.y + 50,0);

        lineRen.SetPosition(1, transform.position);
        while (transform.position.y > target.y)
        { 
            transform.position += -Vector3.up * Time.deltaTime * 50;
            lineRen.SetPosition(0, transform.position);
            yield return null;
        }
        Instantiate(landParticles, transform.position, Quaternion.identity);
        lineRen.enabled = false;
        GetComponent<Collider2D>().enabled = true;

        yield return new WaitForSeconds(2f);
        StartCoroutine(AttackCooldown());
    } 
    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            coll.gameObject.GetComponent<Health>().DealDamage(damage);
        }
    }
}
