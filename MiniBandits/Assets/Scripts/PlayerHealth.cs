using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using EZCameraShake;

public class PlayerHealth : Health
{
    public override void DealDamage(int damage)
    {
        CameraShaker.Instance.ShakeOnce(4f, 4f, .1f, .1f);
        base.DealDamage(damage);
        StartCoroutine(DamageAnimation());
    }
    IEnumerator DamageAnimation()
    {
        transform.localScale = new Vector2(2.2f, 2.2f);

        yield return new WaitForSeconds(0.1f);

        transform.localScale = new Vector2(2f, 2f);
    }
}
