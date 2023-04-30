using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [HideInInspector]
    public int health;
    public int maxHealth;

    public virtual void Awake()
    {
        health = maxHealth;
    }
    public void SetHealth(int h)
    {
        health = h;
    }
    public virtual void DealDamage(int damage)
    {
        health -= damage;
    }
    public virtual void Heal(int healAmt)
    {
        health += healAmt;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }
    public int GetHealth()
    {
        return health;
    }
    public int GetMaxHealth()
    {
        return maxHealth;
    }
    public void SetMaxHealth(int h)
    {
        maxHealth = h;
    } 

    public void SetMaxAndCurrentHealth(int h)
    {
        maxHealth = h;
        health = maxHealth;
    }
    public void AddMaxAndCurrentHealth(int h)
    {
        maxHealth += h;
        health += h;
    }
}
