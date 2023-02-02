using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage  
{
    public bool crit;
    public int damage;
    public GameObject damageSource;

    public Damage(bool c, int d, GameObject s)
    {
        crit = c;
        damageSource = s;
        if (crit)
        {
            damage = (int)(d * 1.5f);
        }
        else
        {
            damage = d;
        }
    }
}
