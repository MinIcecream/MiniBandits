using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOAI : EnemyAI, IAffectable
{  
    public float chaseSpeed;
    bool canStart = false;
    public GameObject beam;

    public override void StartLevel()
    {
        canStart = true;
        StartCoroutine(Fire());
    }
     

    public override void Update()
    {
        base.Update();

        if (player == null || !canStart)
        {
            return;
        } 
        Vector2 dir = (Vector2)player.transform.position - new Vector2(transform.position.x, transform.position.y-2);

        transform.position = (Vector2)transform.position + dir.normalized * chaseSpeed * Time.deltaTime;
    }
    IEnumerator Fire()
    {
        while (true)
        {
            beam.SetActive(true);
            yield return new WaitForSeconds(5f);
            beam.SetActive(false);
            yield return new WaitForSeconds(2f);
        }
    } 
    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            coll.gameObject.GetComponent<Health>().DealDamage(5);
        }
    }
}
