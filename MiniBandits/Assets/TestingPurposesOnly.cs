using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingPurposesOnly : MonoBehaviour
{ 
    void Update()
    {
        if (Input.GetKeyDown("y"))
        {
            GameObject.FindWithTag("Player").GetComponent<Player>().AddHealth(20);
        }
    }
}
