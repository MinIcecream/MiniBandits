using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shrine : Interactable
{ 
    public TextMeshPro tmp;
    public Sprite activatedSprite, deactivatedSprite;
    bool used = false;

    [HideInInspector]
    public ProgressRoomManager roomMan;

    public enum shrineTypes
    {
        vitality,
        strength,
        defense,
        speed
    }
    public shrineTypes shrineType;

    void Awake()
    { 
        tmp.text = "[E to interact]";
    }
    public override void ActivatePopup()
    { 
        if (roomMan.levelComplete == true && !used)
        {
            base.ActivatePopup();
        } 
    } 
    void Update()
    {
        if (roomMan == null)
        {
            return;
        }
        if (roomMan.levelComplete == true&&!used)
        {
            GetComponent<SpriteRenderer>().sprite = activatedSprite; 
        }
        else
        { 
            GetComponent<SpriteRenderer>().sprite = deactivatedSprite;
        }
         
    } 
    public override void Interact()
    {
        used = true;
        popup.SetActive(false);

        GameObject player = GameObject.FindWithTag("Player");
        if (player == null)
        {
            return;
        }

        switch (shrineType)
        {
            case shrineTypes.vitality:
                player.GetComponent<Health>().Heal(200);
                break;
            case shrineTypes.defense:
                player.GetComponent<PlayerStatusEffects>().BuffDefense(10, 10);
                break;
            case shrineTypes.strength:
                player.GetComponent<PlayerStatusEffects>().BuffStrength(10, 10);
                break;
            case shrineTypes.speed:
                player.GetComponent<PlayerStatusEffects>().BuffSpeed(10, 10);
                break;
        }
    }
}
