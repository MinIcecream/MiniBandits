using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionToNextRoom : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().WalkToRoom();
             
 
            Destroy(this);
        }
    }
}
