using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrapyard : EnemyAI, IDamageable
{ 
    public int chaseSpeed; 
    public int numLasers = 8;

    bool canAttack = false;
    string lastAttack = "dash";
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
            transform.position += (player.transform.position - transform.position).normalized * Time.deltaTime * chaseSpeed;
        }
        if (canAttack)
        {
            currentlyAttacking = true;
            if (lastAttack == "dash")
            {
                lastAttack = "laser";
                canAttack = false;
                StartCoroutine(Laser());
            }
            else if (lastAttack == "laser")
            {
                lastAttack = "dash";
                canAttack = false;
                StartCoroutine(Dash());
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
    IEnumerator Laser()
    {
        yield return new WaitForSeconds(1.5f);

        for(int k = 0; k<3; k++)
        {
            StartCoroutine(SpawnLasers(k));
            yield return new WaitForSeconds(0.75f);
        }
         
        StartCoroutine(AttackCooldown());
    }
    IEnumerator SpawnLasers(int k)
    { 
            for (int i = 0; i < numLasers; i++)
            {
                float angle = (i * 360f / numLasers) + k * 8;
                Vector3 direction = Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.right;
                GetComponent<AttackIndicator>().GenerateAttackIndicator(direction);
            }
            yield return new WaitForSeconds(0.5f);

            LineRenderer[] lasers = new LineRenderer[numLasers];
            for (int j = 0; j < numLasers; j++)
            {
                float angle = (j * 360f / numLasers) + k * 8;
                Vector3 direction = Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.right;


                GameObject laserObj = new GameObject($"Laser {j}");
                lasers[j] = laserObj.AddComponent<LineRenderer>();
                lasers[j].material = new Material(Shader.Find("Sprites/Default"));
                lasers[j].material.color = Color.red;
                lasers[j].startWidth = .3f;
                lasers[j].endWidth = .3f;
                lasers[j].positionCount = 2;

                lasers[j].SetPosition(0, transform.position);


                LayerMask collisionMask = LayerMask.GetMask("Player", "Terrain");
                LayerMask collisionMask2 = LayerMask.GetMask("Terrain");
                RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 30, collisionMask);
                RaycastHit2D wallhit = Physics2D.Raycast(transform.position, direction, 30, collisionMask2);
                if (hit.collider.gameObject.tag == "Player")
                {
                    hit.collider.gameObject.GetComponent<Health>().DealDamage(damage);
                }
                lasers[j].SetPosition(1, wallhit.point);
            }

            yield return new WaitForSeconds(0.5f);
            foreach (LineRenderer ren in lasers)
            {
                Destroy(ren.gameObject);
            }
         
    }
    IEnumerator Dash()
    {
        yield return new WaitForSeconds(1.5f);
 
        StartCoroutine(AttackCooldown());
    }

    void DrawRay(Vector2 startPos, Vector2 endPos)
    {
        //lineRen.SetPosition(0, startPos);
       // lineRen.SetPosition(1, endPos);
    }
    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            coll.gameObject.GetComponent<Health>().DealDamage(80);
        }
    }
}
