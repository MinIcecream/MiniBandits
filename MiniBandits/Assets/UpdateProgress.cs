using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateProgress : MonoBehaviour
{
    public ProgressRoomManager levelMan;

    void Update()
    {
        if (levelMan.levelComplete)
        {
            GameManager.CompleteRoom();
            Destroy(this);
        }
    }

}
