using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shrine : MonoBehaviour
{
    public GameObject popup;
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
    void OnTriggerEnter2D(Collider2D coll)
    { 
        if (coll.gameObject.tag != "Player"|| roomMan.levelComplete != true || used)
        {
            return;
        }
        popup.SetActive(true);
        popup.transform.position = new Vector2(transform.position.x, transform.position.y + 1);
    }
    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.tag != "Player")
        {
            return;
        }
        popup.SetActive(false);
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

        if (popup.activeSelf == true && Input.GetKeyDown("e"))
        {
            used = true;
            popup.SetActive(false);

            GameObject player = GameObject.FindWithTag("Player");
            if (player==null)
            {
                return;
            }

            switch (shrineType)
            {
                case shrineTypes.vitality:
                    player.GetComponent<Health>().Heal(20);
                    break;
                case shrineTypes.defense:
                    player.GetComponent<PlayerStatusEffects>().BuffDefense(10,10);
                    break;
                case shrineTypes.strength:
                    player.GetComponent<PlayerStatusEffects>().BuffStrength(10,10);
                    break;
                case shrineTypes.speed:
                    player.GetComponent<PlayerStatusEffects>().BuffSpeed(10,10);
                    break;
            }
        }
    } 
}
