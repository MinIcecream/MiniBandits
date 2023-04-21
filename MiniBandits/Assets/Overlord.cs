using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Overlord : EnemyAI, IDamageable
{
    LineRenderer lineRen;

    public int chaseSpeed;
    public LayerMask raycastMask;

    bool canAttack = false;
    string lastAttack = "shockWave";
    bool currentlyAttacking = false;

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
            if (lastAttack == "shockWave")
            {
                lastAttack = "laser";
                canAttack = false;
                StartCoroutine(Laser());
            }
            else if (lastAttack == "laser")
            {
                lastAttack = "shockWave";
                canAttack = false;
                StartCoroutine(Shockwave());
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
        lineRen.enabled = true;

        float timeElapsed = 0f;

        Vector2 laserDirection = Vector2.down;

        while (timeElapsed < 10)
        {
            timeElapsed += Time.deltaTime;

            if (player == null)
            {
                yield break;
            }
            //Changing directions to shoot lser
            Vector2 targetDirection = player.transform.position - transform.position;
            
            float angle = Vector2.SignedAngle(laserDirection, targetDirection);
            float rotateAngle = 0.25f;

            if (angle > rotateAngle)
            {
                laserDirection = Quaternion.Euler(0, 0, rotateAngle) * laserDirection;
            }
            else if (angle < -rotateAngle)
            {
                laserDirection = Quaternion.Euler(0, 0, -rotateAngle) * laserDirection;
            }
            else
            {
                laserDirection = targetDirection.normalized;
            }

            RaycastHit2D hit = Physics2D.Raycast(transform.position, laserDirection, 30, raycastMask); 
            DrawRay(transform.position, hit.point);
            if (hit.collider.gameObject.tag == "Player")
            {
                Debug.Log("sus");
                hit.collider.gameObject.GetComponent<Health>().DealDamage(damage);
            }
            yield return null;
        }

        lineRen.enabled = false;
         
        StartCoroutine(AttackCooldown());
    }
    IEnumerator Shockwave()
    {
        yield return new WaitForSeconds(1.5f);
        
        StartCoroutine(AttackCooldown());
    }

    void DrawRay(Vector2 startPos, Vector2 endPos)
    {
        lineRen.SetPosition(0, startPos);
        lineRen.SetPosition(1, endPos);
    }
    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            coll.gameObject.GetComponent<Health>().DealDamage(80);
        }
    }
}
