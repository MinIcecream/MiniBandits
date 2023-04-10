using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SporeProjectile : BaseProjectile
{
    public float dragMax, dragMin;
     
    public override void Awake()
    {
        base.Awake();
        rb.drag = Random.Range(dragMin, dragMax);
    }
}
