using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWhenNotInGame : MonoBehaviour
{
    public static DestroyWhenNotInGame instance;

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
