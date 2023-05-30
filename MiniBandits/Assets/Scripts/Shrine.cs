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
        GameObject player = GameObject.FindWithTag("Player");
        if (player == null||used)
        {
            return;
        }
        used = true;
        popup.SetActive(false);
         

        switch (shrineType)
        {
            case shrineTypes.vitality:
                int healAmt = (int)(.30f * player.GetComponent<Health>().GetMaxHealth());
                player.GetComponent<Health>().Heal(healAmt);
                PopupManager.SpawnPopup(transform.position, "healed!",false);
                break;
            case shrineTypes.defense:
                player.GetComponent<PlayerStatusEffects>().BuffDefense(10, 10);
                PopupManager.SpawnPopup(transform.position, "Defense Up!", false);
                break;
            case shrineTypes.strength:
                player.GetComponent<PlayerStatusEffects>().BuffStrength(10, 10);
                PopupManager.SpawnPopup(transform.position, "Strength Up!", false);
                break;
            case shrineTypes.speed:
                player.GetComponent<PlayerStatusEffects>().BuffSpeed(10, 10);
                PopupManager.SpawnPopup(transform.position, "Speed Up!", false);
                break;
        }
    }
}
