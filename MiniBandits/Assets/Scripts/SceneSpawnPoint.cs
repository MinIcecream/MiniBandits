using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSpawnPoint : MonoBehaviour
{ 
    void Start()
    { 
        DontDestroyOnLoad(gameObject);
    } 
}
