using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    GameObject player;
     
    void Start()
    {
        player = GameObject.FindWithTag("Player"); 
    }
    void Update()
    {
        if (player == null)
        {
            return;
        }
        transform.position = player.transform.position;
    }    
}
