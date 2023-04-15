using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxingGlove : BaseProjectile
{
    public float maxDistance;

    [HideInInspector]
    public PunchGun parent;
    [HideInInspector]
    public Transform player;
    public LineRenderer lineRen;
    bool endEarly = false;

    public override void SetDir(Vector2 targetPt)
    {
        player = GameObject.FindWithTag("Player").transform;
        Vector2 direction = ((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position).normalized;
        transform.right = -direction;
        Vector2 targetPosition = direction * maxDistance + (Vector2)transform.position;
        StartCoroutine(GoToTarget(targetPosition));
    }
    IEnumerator GoToTarget(Vector2 target)
    {
        //While it's far away, mmove towards it and draw line
        while (Vector2.Distance(transform.position, target) > 0.2f)
        {
            if (parent == null)
            {
                Destroy(gameObject);
                yield break;
            }
            if (endEarly)
            {
                break;
            }

            Vector2 dir = (target - (Vector2)transform.position).normalized;
            transform.position += (Vector3)dir * Time.deltaTime * speed;

            lineRen.SetPosition(0, parent.transform.position);
            lineRen.SetPosition(1, transform.position);

            yield return null;
        }

        if (parent == null)
        {
            Destroy(gameObject);
            yield break;
        }

        //While if it smore than 1 away, 
        while (Vector2.Distance(player.position, player.position) > 0.6f)
        {
            if (parent == null)
            {
                Destroy(gameObject);
                yield break;
            } 

            Vector2 dir = ((Vector2)parent.gameObject.transform.position - (Vector2)transform.position).normalized;
            transform.position += (Vector3)dir * Time.deltaTime * speed*1.5f;

            lineRen.SetPosition(0, parent.transform.position);
            lineRen.SetPosition(1, transform.position);

            yield return null;
        }
        parent.gloveFired = false;
        Destroy(gameObject);
    }
    public override void OnTriggerEnter2D(Collider2D coll)
    {
        endEarly = true; 
        GameObject obj = coll.gameObject;

        if (obj.GetComponent<Health>() != null)
        {
            if (obj.GetComponent<IDamageable>() != null)
            {
                obj.GetComponent<IDamageable>().Damage(damage);
            }
            if (obj.GetComponent<IAffectable>() != null)
            {
                obj.GetComponent<IAffectable>().Knockback(knockBackAmt, transform.position);
            }
            InstantiateParticles();

            if (destroyOnHit)
            {
                Destroy(gameObject);
            }
        }
        if (obj.tag == "Wall")
        {
            if (destroyOnWall)
            {
                InstantiateParticles();
                Destroy(gameObject);
            }
        }
    }

}