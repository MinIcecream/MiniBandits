using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderWeb : BaseProjectile
{
    GameObject player;
     
    public override void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            player = coll.gameObject;
            player.GetComponent<PlayerStatusEffects>().Slow(2);
            Destroy(gameObject);
        }
    } 
}
