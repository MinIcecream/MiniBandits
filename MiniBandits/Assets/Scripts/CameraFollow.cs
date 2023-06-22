using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    GameObject player;
     bool shouldFollow=true;

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
        if(shouldFollow)
        { 
         transform.position = player.transform.position;
        } 
    }    
    public void StopFollowing(){
        shouldFollow=false;
    }
    public void Follow(){
        shouldFollow=true;
    }
}
