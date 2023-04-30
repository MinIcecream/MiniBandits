using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cacoon : MonoBehaviour, IDamageable
{
    Health health;
    public GameObject spider;
    public float destructDelay;

    void Awake()
    {
        health = GetComponent<Health>();
        StartCoroutine(SelfDestruct());
    }
    public void Damage(int damage)
    {
        GetComponent<Health>().DealDamage(damage);
        StartCoroutine(DamageAnimation());
    }
    IEnumerator DamageAnimation()
    {
        transform.localScale = new Vector2(1.2f, 1.2f);

        yield return new WaitForSeconds(0.1f);

        transform.localScale = new Vector2(1f, 1f);
    }

    void Update()
    {
        if (health.GetHealth() <= 0)
        {
            SpawnSpiders();
            Destroy(gameObject);
        }
    } 
    void SpawnSpiders()
    { 
        for (int i = 0; i < Random.Range(1, 3); i++)
        {
            var newSpider = Instantiate(spider, transform.position, Quaternion.identity);
            SpiderAI spiderAI = newSpider.GetComponent<SpiderAI>();
            Health spiderHealth = newSpider.GetComponent<Health>();
            spiderAI.StartLevel();
            spiderAI.damage = (int)(spiderAI.damage / 1.5f);
            spiderHealth.SetMaxAndCurrentHealth((int)(spiderHealth.GetMaxHealth() / 1.5f));
        }
    }
    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(destructDelay);
        SpawnSpiders();
        Destroy(gameObject);
    }
}
