using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : Interactable
{
    public Door parent;

    public override void ActivatePopup()
    {

    }
    public override void DeactivatePopup()
    {

    }
    public override void Interact()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player == null)
        {
            return;
        }

        KeyManager man = player.GetComponent<KeyManager>();

        if (man.GetKeys() >= 1)
        {
            man.SpendKeys(1);
            parent.Unlock();
        }
        else
        {
            Debug.Log("NO KEYS???");
        }
    }
}
