using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    public float destructDelay;

    void Awake()
    {
        Invoke("Destroy", destructDelay);
    }
    void Destroy()
    {
        Destroy(gameObject);
    }
}
