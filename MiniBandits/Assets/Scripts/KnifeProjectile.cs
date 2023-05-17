using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeProjectile : BaseProjectile
{ 
    [HideInInspector]
    public Knife parent;
    [HideInInspector]
    public Transform player; 
    bool endEarly = false;

    public override void SetDir(Vector2 targetPt)
    {
        player = GameObject.FindWithTag("Player").transform;
        Vector2 direction = ((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position).normalized;
        transform.right = -direction;
        Vector2 targetPosition = direction * range + (Vector2)transform.position;
        StartCoroutine(GoToTarget(targetPosition));
    }
    IEnumerator GoToTarget(Vector2 target)
    {
        Debug.Log(target);
        //GO TO TARGET
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
            transform.position += (Vector3)((Vector2)dir * Time.deltaTime * speed); 

            yield return null;
        }

        if (parent == null)
        { 
            Destroy(gameObject);
            yield break;
        }

        //RETURN TO PLAYER
        while (Vector2.Distance(transform.position, player.position) > 1f)
        { 
            if (parent == null)
            { 
                Destroy(gameObject);
                yield break;
            }

            Vector2 dir = ((Vector2)parent.gameObject.transform.position - (Vector2)transform.position).normalized;
            transform.position += (Vector3)dir * Time.deltaTime * speed * 1.5f; 

            yield return null;
        }
        parent.knifeFired = false;
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
                obj.GetComponent<IAffectable>().Knockback(knockBack, transform.position);
            }
            InstantiateParticles();
             
        }
        if (obj.tag == "Wall")
        { 
            InstantiateParticles();
        }
    }  
    public override void FixedUpdate()
    {

    }
}