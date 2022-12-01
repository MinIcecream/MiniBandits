using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject level;
     
    void Start()
    {
        level.transform.position = GameObject.FindWithTag("SceneSpawnPoint").transform.position;
    } 
}
