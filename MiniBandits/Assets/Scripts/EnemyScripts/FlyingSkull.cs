using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingSkull : EnemyAI, IAffectable
{
    SpriteRenderer sprite;
    [SerializeField] Transform flamePt;
    [SerializeField] Transform flipYPt;

    [SerializeField] GameObject flames;
     
    public float chaseSpeed;
    bool canStart = false;
    public float attackDistance;
     
    public override void StartLevel()
    {
        canStart = true;
        StartCoroutine(Fire());
    }

    public override void Awake()
    {
        base.Awake();
        sprite = GetComponent<SpriteRenderer>();
    } 
  
    public override void Update()
    {
        base.Update();

        if (player == null || !canStart)
        {
            return;
        }
        if (Vector2.Distance(transform.position, player.transform.position) > attackDistance)
        {

            Vector2 dir = player.transform.position - transform.position;

            transform.position = (Vector2)transform.position + dir.normalized * chaseSpeed * Time.deltaTime;
        } 
        FacePlayer();
    }
    IEnumerator Fire()
    {
        while (true)
        {
            flames.SetActive(true);
            yield return new WaitForSeconds(5f);
            flames.SetActive(false);
            yield return new WaitForSeconds(2f);
        }
    }
    public void FacePlayer()
    {
        transform.right = transform.position - player.transform.position;
        if (transform.right.x < 0)
        {
            flames.transform.SetParent(flipYPt);
            sprite.flipY = true;
        }
        else
        {
            flames.transform.SetParent(flamePt);
            sprite.flipY = false;
        }
        flames.transform.localPosition = Vector3.zero;
    }
    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            coll.gameObject.GetComponent<Health>().DealDamage(5);
        }
    }
}
