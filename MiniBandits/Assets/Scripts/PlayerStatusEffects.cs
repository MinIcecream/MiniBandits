using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusEffects : MonoBehaviour
{
    [HideInInspector]
    public bool slow = false;
    float slowTimeoutTime = 0;

    GameObject player;

    void Awake()
    {
        player = GameObject.FindWithTag("Player");
    }

    public void Slow(float duration)
    {
        slow = true;

        float newSlowTimeoutTime = Time.time + duration;
         
        if (newSlowTimeoutTime > slowTimeoutTime)
        {
            slowTimeoutTime = newSlowTimeoutTime;
        }
    }

    void Update()
    {
        if (Time.time > slowTimeoutTime)
        { 
            slow = false;
        }

        if (slow)
        { 
            player.GetComponent<PlayerMovement>().movementSpeed = 0.5f;
        }
        else
        {
            player.GetComponent<PlayerMovement>().movementSpeed = 1f;
        }
    }
}
