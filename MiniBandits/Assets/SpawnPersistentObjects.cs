using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPersistentObjects : MonoBehaviour
{
    public GameObject persistentObjs;

    void Start()
    {
        if (GameObject.FindWithTag("PersistBetweenFloors") == null)
        {
            GameObject newPersistents=Instantiate(persistentObjs, transform.position, Quaternion.identity);
            newPersistents.transform.SetParent(this.transform, true);
        }
    }
     
}
