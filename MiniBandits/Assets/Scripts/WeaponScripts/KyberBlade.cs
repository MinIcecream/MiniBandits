using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class KyberBlade : WeaponTemplate
{ 
    List<GameObject> hitEnemies = new List<GameObject>();
    
    public override void WhileAttacking()
    { 
        GetComponent<Collider2D>().enabled = true;
        transform.Rotate(Vector3.forward * Time.deltaTime * 1500);
    }
    public override void StopAttack()
    {
        GetComponent<Collider2D>().enabled = false;
    }
    public override void Update()
    {
        //IF PLAYER IS GONE, PLAYER CAN't MOVE, OR MOUSE IS OVER UI, RETURN.
        if (player == null || !player.canMove)
        {
            return;
        }
        UpdateStats(); 
          
        GetComponent<CircleCollider2D>().radius = AOE;
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
                obj.GetComponent<IAffectable>().Knockback(knockBack, transform.position);
            }
            hitEnemies.Add(coll.gameObject);
            StartCoroutine(RemoveFromList(coll.gameObject));
        }
    }
    IEnumerator RemoveFromList(GameObject obj)
    {
        yield return new WaitForSeconds(1/weapon.attackSpeed);
        hitEnemies.Remove(obj);
    }
}
