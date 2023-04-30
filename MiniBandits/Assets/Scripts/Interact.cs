using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    //Used to turn off popups
    Interactable lastInteractable;

    void Update()
    {
        MonoBehaviour[] scripts = FindObjectsOfType<Interactable>();

        if (lastInteractable != null)
        {
            lastInteractable.DeactivatePopup();
        }
        if (scripts.Length <= 0)
        {
            return;
        }
        float closestDistance = Mathf.Infinity;
        Interactable closestScript = (Interactable)scripts[0];
        foreach(Interactable script in scripts)
        {
            float dist = Vector2.Distance(script.gameObject.transform.position, transform.position);

            if (dist < closestDistance)
            {
                closestDistance = dist;
                closestScript = script;
            }
        }

        if (Vector2.Distance(closestScript.gameObject.transform.position, transform.position) <= 3)
        {
            lastInteractable = closestScript;
            closestScript.ActivatePopup();

            if (Input.GetKeyDown("e"))
            {
                Debug.Log("FDF");
                closestScript.Interact();
            }
        }
    }
}
