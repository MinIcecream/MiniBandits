using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour
{
    public RectTransform fader;

    public void TransitionToGame()
    {
        fader.gameObject.SetActive(true);

        LeanTween.scale(fader, Vector3.zero, 0f).setIgnoreTimeScale(true);
        LeanTween.scale(fader, new Vector3(1, 1, 1), 0.5f).setEase(LeanTweenType.easeInOutQuad).setIgnoreTimeScale(true);
        Invoke("LoadLevelScene", 1f);
    }
    void LoadLevelScene()
    {
        SceneManager.LoadScene("Levels");
    }
}
