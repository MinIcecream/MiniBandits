using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Interact : MonoBehaviour,IPointerDownHandler, IPointerUpHandler
{ 
    //INTERACT BUTTON IF PRESSED:
    [HideInInspector]
    public bool isPressed;

    public Sprite attack, interact;

    public enum options
    {
        interact,
        attack
    }
    public options option;

   public FindInteractableObject objMan;

    public Image img;

    PlayerMovement player;
    void Awake()
    { 
        GameObject playerObj = GameObject.FindWithTag("Player");
        player = playerObj.GetComponent<PlayerMovement>();
    }
    void Update()
    {
        if (player == null)
        {
            return;
        }
        if (objMan.GetClosestInteractable() != null && !player.inCombat)
        {
            option = options.interact;
        }
        else
        {
            option = options.attack;
        }

        if (isPressed)
        { 
            if (option == options.attack)
            {
                if (GameObject.FindWithTag("Player") != null)
                { 
                    GameObject.FindWithTag("WeaponSpawnPt").transform.GetChild(0).GetComponent<WeaponTemplate>().GetAttackInput(); 
                } 
            }
        }

        switch (option)
        {
            case options.interact:
                if (!player.inCombat)
                { 
                    img.sprite = interact;
                } 
                break;
            case options.attack:
                img.sprite = attack;
                break;
        }
    }
    //JUST ONCE WHEN THE BUTTON IS FIRST PRESED:
    public void ClickButton()
    {
        Interactable obj = objMan.GetClosestInteractable();
        if (option == options.interact && obj!=null)
        {
    //        Debug.Log("HUH");
            obj.Interact();
        } 
    }

    //CONTINUOUSLY WHILE BUTTON IS PRESSED:
    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true; 
    }

    //WHEN BUTTON IS RELEASED:
    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;
        if (option == options.attack)
        {
            if (GameObject.FindWithTag("Player") != null)
            { 
                GameObject.FindWithTag("WeaponSpawnPt").transform.GetChild(0).GetComponent<WeaponTemplate>().StopAttack();
            } 
        }
    }
}
