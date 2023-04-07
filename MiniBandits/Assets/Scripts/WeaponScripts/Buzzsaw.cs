using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buzzsaw : BaseProjectile
{ 
    void LateUpdate()
    {
        transform.Rotate(new Vector3(0, 0, 1800 * Time.deltaTime));
    }
}
