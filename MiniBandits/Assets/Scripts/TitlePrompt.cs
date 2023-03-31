using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitlePrompt : MonoBehaviour
{
    [SerializeField]
    float speed;
    string state="shrinking";
    
    void FixedUpdate()
    {
        if(state=="shrinking")
        {
            transform.localScale-=new Vector3(speed,speed,0);
        }
        else
        {
            transform.localScale+=new Vector3(speed,speed,0);
        }

        if(transform.localScale.x>1.15f){
            state="shrinking";
        }
        else if (transform.localScale.x<0.85f){
            state="growing";
        }
    }
}
