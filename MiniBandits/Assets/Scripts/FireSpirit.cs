using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpirit : MonoBehaviour
{
    int damage = 20;
    List<IDamageable> damageables = new List<IDamageable>();

    void Awake()
    {
        Invoke("Explode", 5f);
    }
    void Update()
    {
        if (findClosestEnemy().x != 1000)
        { 
            transform.position = Vector2.MoveTowards(transform.position, findClosestEnemy(), 5 * Time.deltaTime);
            if (Vector2.Distance(transform.position, findClosestEnemy()) < 1)
            { 
                Explode();
            }
        } 
    }
    private Vector2 findClosestEnemy()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closestEnemy = null;
        float closestDistance = 100;
        bool first = true;

        foreach (var obj in objs)
        { 
            float distance = Vector2.Distance(obj.transform.position, transform.position);
            if (first)
            {
                closestDistance = distance; 
                closestEnemy = obj;
                first = false;
            }
            else if (distance < closestDistance)
            {
                closestEnemy = obj;
                closestDistance = distance;
            }

        } 
        if (null == closestEnemy)
        {
            return new Vector2(1000, 1000);
        }
        return closestEnemy.transform.position;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        IDamageable hitObj = coll.gameObject.GetComponent<IDamageable>();

        if (hitObj != null)
        {
            if (!damageables.Contains(hitObj))
            {
                damageables.Add(hitObj);
            }
        }
    }
    void OnTriggerExit2D(Collider2D coll)
    {
        IDamageable hitObj = coll.gameObject.GetComponent<IDamageable>();

        if (hitObj != null)
        {
            if (damageables.Contains(hitObj))
            {
                damageables.Remove(hitObj);
            }
        }
    }
    void Explode()
    {
        Instantiate(Resources.Load<GameObject>("Misc/FireSpiritParticles"), transform.position, Quaternion.identity);
        foreach (IDamageable opp in damageables)
        {
            if (opp != null)
            {
                opp.Damage(damage);
            } 
        }
        Destroy(gameObject);
    }
}
