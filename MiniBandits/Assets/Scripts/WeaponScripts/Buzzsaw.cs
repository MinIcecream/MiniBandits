using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buzzsaw : TESTPlayerProjectile
{
    public override void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.GetComponent<Health>() != null)
        {
            coll.gameObject.GetComponent<IDamageable>().Damage(damage);
            Instantiate(particles, transform.position, Quaternion.identity); 
        } 
    } 
    void LateUpdate()
    {
        transform.Rotate(new Vector3(0, 0, 1800 * Time.deltaTime));
    }
}
