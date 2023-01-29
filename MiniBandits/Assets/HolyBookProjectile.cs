using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyBookProjectile : MonoBehaviour
{
    public int damage;
    void Update()
    {
        transform.eulerAngles = Vector3.zero;
    }
    public virtual void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.GetComponent<Health>() != null)
        {
            coll.gameObject.GetComponent<IDamageable>().Damage(damage); 
        } 
    }
}
