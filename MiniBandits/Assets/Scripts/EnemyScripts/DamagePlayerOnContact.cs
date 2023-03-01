using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayerOnContact : MonoBehaviour
{
    public int damage;
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        { 
            coll.gameObject.GetComponent<IDamageable>().Damage(damage);
        }
    }
}
