using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasDontDestroyOnLoad : MonoBehaviour
{
    public static CanvasDontDestroyOnLoad instance;

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
