using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private int health;
    public int maxHealth;

    void Start()
    {
        health = maxHealth;
    }

    public virtual void DealDamage(int damage)
    {
        health -= damage;
    }
    public int GetHealth()
    {
        return health;
    }
}
