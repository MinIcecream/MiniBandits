using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderWeb : MonoBehaviour
{
    GameObject player;
     
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            player = coll.gameObject;
            StartCoroutine(Slow());
        }
    }
    IEnumerator Slow()
    {
        float startTime = Time.time;
        while (Time.time - startTime < 2)
        {
            if (player!= null)
            { 
                player.GetComponent<Rigidbody2D>().drag = 10;
                yield return null;
            } 
        } 
        if (player != null)
        { 
            player.GetComponent<Rigidbody2D>().drag = 0;
        } 
    }

    void OnDisable()
    {
        if (player)
        { 
            player.GetComponent<Rigidbody2D>().drag = 0;
        } 
    }
}
