using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacePlayer : MonoBehaviour
{
    public GameObject player;
    Vector2 lastPlayerPos;

    void Awake()
    {
        Invoke("CreateReferences", 0.2f);
    }
    void LateUpdate()
    {
        if (player == null)
        {
            if (lastPlayerPos.x > transform.position.x)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(new Vector3(0, 180, 0)), 500 * Time.deltaTime);
            }
            else
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(new Vector3(0, 0, 0)), 500 * Time.deltaTime);
            }
            return;
        }
        if (player.transform.position.x > transform.position.x)
        { 
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(new Vector3(0,180,0)), 500 * Time.deltaTime);
        }
        else
        { 
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(new Vector3(0, 0, 0)), 500 * Time.deltaTime); 
        }
        lastPlayerPos = player.transform.position;
    }
    void CreateReferences()
    {
        player = GameObject.FindWithTag("Player");
    }
}
