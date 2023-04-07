using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KyberBlade : WeaponTemplate
{
    public int knockBackAmt;
    List<GameObject> hitEnemies = new List<GameObject>();
    public override void Update()
    {
        base.Update();
        if (Input.GetMouseButton(0))
        {
            GetComponent<Collider2D>().enabled = true;
            transform.Rotate(Vector3.forward * Time.deltaTime * 1500);
        }
        if (Input.GetMouseButtonUp(0))
        { 
            GetComponent<Collider2D>().enabled = false;
        }
    } 
    void OnTriggerStay2D(Collider2D coll)
    {
        GameObject obj = coll.gameObject;
        if (hitEnemies.Contains(coll.gameObject))
        {
            return;
        } 
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
            hitEnemies.Add(coll.gameObject);
            StartCoroutine(RemoveFromList(coll.gameObject));
        }
    }
    IEnumerator RemoveFromList(GameObject obj)
    {
        yield return new WaitForSeconds(0.3f);
        hitEnemies.Remove(obj);
    }
}
