using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnablePauseButton : MonoBehaviour
{
    public GameObject button;

    void Update()
    {
        if (!GameObject.FindWithTag("Player"))
        {
            return;
        }
        PlayerMovement player = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
        if (player.inCombat)
        {
            button.SetActive(false);
        }
        else
        {
            button.SetActive(true);
        }
    }
}
