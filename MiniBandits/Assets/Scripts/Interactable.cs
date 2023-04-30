using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable:MonoBehaviour
{
    public GameObject popup;

    public virtual void Interact()
    {

    }
    public virtual void ActivatePopup()
    { 
        popup.SetActive(true);
        popup.transform.position = new Vector2(transform.position.x, transform.position.y + 1);
    }
    public virtual void DeactivatePopup()
    { 
        popup.SetActive(false);
    }
}
