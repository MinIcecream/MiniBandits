using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SealEntrances : MonoBehaviour
{
    public LevelManager levelMan;

    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            levelMan.SealEntrances(); 
            GameObject.FindWithTag("CameraParent").transform.position = new Vector2(transform.position.x, transform.position.y + 4.5f);

            GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().ReachedRoom();
            Destroy(this.gameObject); 
        }
    }
}
