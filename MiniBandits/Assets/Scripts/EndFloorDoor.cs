using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndFloorDoor : MonoBehaviour
{    
    void OnTriggerStay2D(Collider2D coll)
    { 
        if (coll.gameObject.tag == "Player")
        {
            Debug.Log("LEAVING...");
            GameObject.FindWithTag("GameManager").GetComponent<GameManager>().GenerateNewFloor(); 
            Destroy(this);
        }
    }
}
