using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistBetweenFloors : MonoBehaviour
{
    public static PersistBetweenFloors instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

    }
}
