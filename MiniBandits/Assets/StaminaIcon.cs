using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaIcon : MonoBehaviour
{
    public Image image;

    public void Activate()
    {
        image.color = new Color32(255, 255, 0, 100);
    }
    public void DeActivate()
    { 
        image.color = new Color32(255, 255, 255, 100);
    }
}
