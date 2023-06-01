using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaIcon : MonoBehaviour
{
    public Image image;

    public void Activate()
    {
        image.color = new Color32(194, 192, 0, 255);
    }
    public void DeActivate()
    { 
        image.color = new Color32(0, 0, 0, 255);
    }
}
