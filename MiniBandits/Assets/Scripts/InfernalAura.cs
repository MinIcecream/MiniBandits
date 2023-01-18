using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfernalAura : MonoBehaviour
{
    public InfernalHelmet helmet;
    public InfernalChestplate chestplate;
    public InfernalPants pants;

    public int baseDamage;

    ParticleSystem.ShapeModule ps;
    int damage;
    public int baseRadius;
    List<IDamageable> damageables = new List<IDamageable>();

    void Awake()
    { 
        StartCoroutine(Burn());
        ps = GetComponent<ParticleSystem>().shape;
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
    void Update()
    {
        int numStacks = 0;

        if (helmet != null)
        {
            numStacks++;
        }
        if (chestplate != null)
        {
            numStacks++;
        }
        if (pants != null)
        {
            numStacks++;
        }
        if (numStacks == 0)
        {
            Destroy(gameObject);
        }

        switch (numStacks)
        {
            case 1:
                damage = baseDamage;
                GetComponent<CircleCollider2D>().radius = baseRadius;
                ps.radius = baseRadius;
                break;
            case 2:
                damage = (int)(1.5*baseDamage);
                GetComponent<CircleCollider2D>().radius = (1.5f*baseRadius);
                ps.radius = (1.5f*baseRadius);
                break;
            case 3:
                damage = 2*baseDamage;
                GetComponent<CircleCollider2D>().radius = 2*baseRadius;
                ps.radius = 2*baseRadius;
                break;
        }
    }
    IEnumerator Burn()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            foreach(IDamageable opp in damageables)
            {
                opp.Damage(damage);
            }
        }
    }
}
