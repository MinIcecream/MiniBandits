using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaintainScale : MonoBehaviour
{ 
    public float padding = 5f; 

    void Awake()
    {
        GameObject objectToFollow = GameObject.FindWithTag("PlayerChest");
        // Calculate the minimum camera size required to fit the object on screen with padding
        float objectHeight = objectToFollow.GetComponent<Renderer>().bounds.size.y;
        float objectWidth = objectToFollow.GetComponent<Renderer>().bounds.size.x;
        float cameraHeight = 2f * padding + objectHeight;
        float cameraWidth = 2f * padding + objectWidth;
        float targetCameraSize = Mathf.Max(cameraHeight / 2f, cameraWidth / (2f * Camera.main.aspect));

        // Set the camera's size to the calculated size
        Camera.main.orthographicSize = targetCameraSize;
    }
}
