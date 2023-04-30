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

    public void BuffStrength(int amt, int numRooms)
    { 
        player.GetComponent<Player>().baseStrength += amt;
        int floor = GameManager.floor + Mathf.FloorToInt(numRooms/10f);
        int room = GameManager.room +  numRooms%10 + 1;
        StartCoroutine(BuffStrength(amt, floor, room));
    }
    public void BuffSpeed(int amt, int numRooms)
    { 
        player.GetComponent<Player>().baseSpeed += amt;
        int floor = GameManager.floor + Mathf.FloorToInt(numRooms / 10f);
        int room = GameManager.room + numRooms % 10 + 1;
        StartCoroutine(BuffSpeed(amt, floor, room));
    }
    public void BuffDefense(int amt, int numRooms)
    {
        player.GetComponent<Player>().baseDefense += amt;
        int floor = GameManager.floor + Mathf.FloorToInt(numRooms / 10f);
        int room = GameManager.room + numRooms % 10 + 1;
        StartCoroutine(BuffDefense(amt, floor, room)); 
    }

    IEnumerator BuffStrength(int amt, int floor, int room)
    {
        while (true)
        { 
            if (GameManager.floor == floor && GameManager.room == room)
            {
                player.GetComponent<Player>().baseStrength -= amt;
                yield break;
            }
            yield return null;
        } 
    }
    IEnumerator BuffDefense(int amt, int floor, int room)
    {
        while (true)
        {
            if (GameManager.floor == floor && GameManager.room == room)
            {
                player.GetComponent<Player>().baseDefense -= amt;
                yield break;
            }
            yield return null;
        }
    }
    IEnumerator BuffSpeed(int amt, int floor, int room)
    {
        while (true)
        {
            if (GameManager.floor == floor && GameManager.room == room)
            {
                player.GetComponent<Player>().baseSpeed -= amt;
                yield break;
            }
            yield return null;
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
