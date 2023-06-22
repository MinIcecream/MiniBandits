using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    GameObject player;
    public Joystick joystick;

    void Awake(){
        player=GameObject.FindWithTag("Player");
    }

    public void OnClick(){

        if(player==null){
            return;
        }
        Vector2 dir = joystick.input;
        if(dir==Vector2.zero){
            return;
        }
        //DASH
        if (player.GetComponent<PlayerStamina>().GetStamina()>0)
        {
            player.GetComponent<PlayerMovement>().Dash(dir);
        }
    }
}
