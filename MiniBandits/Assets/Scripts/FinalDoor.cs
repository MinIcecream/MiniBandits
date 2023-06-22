using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalDoor : MonoBehaviour
{
    public GameObject text;

    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            Invoke("DisplayText", 1f);
            GameObject.FindWithTag("CameraParent").GetComponent<CameraFollow>().StopFollowing();
            coll.gameObject.GetComponent<PlayerMovement>().enabled = false;
            coll.gameObject.transform.position = new Vector2(coll.gameObject.transform.position.x, coll.gameObject.transform.position.y + 2000);
        }
    }
    void DisplayText()
    { 
        text.SetActive(true);
        Invoke("LoadMenu",5f);
    }
    void LoadMenu(){
        SceneManager.LoadScene("Menu");
    }
}
