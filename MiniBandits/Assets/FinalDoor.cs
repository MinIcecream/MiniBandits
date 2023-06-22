using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalDoor : MonoBehaviour
{
    public GameObject text;

    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            Invoke("DisplayText", 1f);
            coll.gameObject.GetComponent<PlayerMovement>().enabled = false;
            coll.gameObject.transform.position = new Vector2(coll.gameObject.transform.position.x, coll.gameObject.transform.position.y + 2000);
        }
    }
    void DisplayText()
    { 
        text.SetActive(true);
    }
}
