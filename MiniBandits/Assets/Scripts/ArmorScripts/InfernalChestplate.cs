using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfernalChestplate : ArmorTemplate
{
    void Awake()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (GameObject.FindWithTag("InfernalAura") == null)
        {
            GameObject newAura = (GameObject)Instantiate(Resources.Load("Misc/InfernalAura"), player.transform.position, Quaternion.identity);
            newAura.transform.SetParent(player.transform, false);
            newAura.transform.localPosition = Vector3.zero;
        }

        InfernalAura aura = GameObject.FindWithTag("InfernalAura").GetComponent<InfernalAura>();
        aura.chestplate = this;
    }
}
