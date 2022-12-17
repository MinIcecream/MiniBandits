using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static int floor;
    public static int room;

    public RectTransform fader;
    public GameObject closedWall, openedWall;
    /*
        void Start()
        { 
            Time.timeScale = 0f;
         
            fader.gameObject.SetActive(true);

            LeanTween.scale(fader, new Vector3(1, 1, 1), 0).setIgnoreTimeScale(true);
            LeanTween.scale(fader, Vector3.zero, 1.5f).setIgnoreTimeScale(true).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() =>
            {
                fader.gameObject.SetActive(false);
            });
        
    StartCoroutine(ResumeGame());
        } 

        IEnumerator ResumeGame()
        {
            yield return new WaitForSecondsRealtime(1f);
            Time.timeScale = 1f;
        }
*/
    public static GameManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

    }
    void Update()
    {
        //IF PLAYER DIED, LOAD MENU
        if (GameObject.FindWithTag("Player") == null)
        {
            Invoke("LoadMenu", 1f);
        }

        if (room >= 10)
        {
            Debug.Log("Floor Completed!");
        }
    }
    void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public static void CompleteRoom()
    {
        room++;
    }
}
