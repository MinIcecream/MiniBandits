using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopupManager : MonoBehaviour
{
    [SerializeField]
    GameObject popup;
    static PopupManager man;

    void Awake()
    {
        man = this;
    }
    public static void SpawnPopup(Vector2 pos, string dmg, bool crit)
    { 
        GameObject popupObj = Instantiate(man.popup, pos, Quaternion.identity);

        Color newColor;
        if(crit)
        { 
            newColor = new Color(255, 0, 0);
        }
        else
        { 
            newColor = new Color(255, 255, 255);
        }
        popupObj.GetComponent<DamagePopup>().SetUp(newColor, dmg);
    } 
}
