using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int health;
    public int damage;
    public int crit;
    public int luck;
     
    //SET HEALTH TO A CERTAIN NUMBER
    public void SetHealth(int h)
    {
        health = h;
    }

    //ADD OR SUBTRACT A NUMBER FROM THE MAX HEALTH
    public void AddHealth(int h)
    {
        health += h;
    }

    public int GetHealth()
    {
        return health;
    }
}
