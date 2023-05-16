 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using RoomInfo;

public class GameManager : MonoBehaviour
{
    public static int floor=1;
    public static int room=0;

    public RectTransform fader;
    public GameObject closedWall, openedWall;

    static roomThemes[] themes;
    public static roomThemes currentTheme;

    public roomThemes[] ForDisplayOnly;

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


    //Initially spawn player in a starter room
    void Awake()
    {
        themes = RoomOptionGenerator.GenerateRoomThemes(10);
        ForDisplayOnly = themes; 
        currentTheme = themes[floor-1];
        FloorManager floorMan = GameObject.FindWithTag("FloorManager").GetComponent<FloorManager>();

        roomConfig starterRoom = new roomConfig();
        starterRoom.reward = rewardTypes.starter;
        starterRoom.progressRoom = false;

        floorMan.SpawnRoom(starterRoom);
        floorMan.UpdatePlayerAndCameraPos();
    }

    public void LoadNewFloorScene()
    {  
        SceneManager.LoadScene("Levels");

        currentTheme = themes[floor-1];
        Invoke("GenerateStarterRoomAfterDelay", 0.1f);
    }

    public void GenerateNewFloor()
    {   
        SceneManager.LoadScene("FloorTransition");
        StartCoroutine(IncrementFloor());
        Invoke("LoadNewFloorScene", 2.5f);
    }
    IEnumerator IncrementFloor()
    {
        yield return null;
        room = 0;
        floor++;
    }
    void GenerateStarterRoomAfterDelay()
    { 
        FloorManager floorMan = GameObject.FindWithTag("FloorManager").GetComponent<FloorManager>();

        roomConfig starterRoom = new roomConfig();
        starterRoom.reward  = rewardTypes.starter;
        starterRoom.progressRoom = false;

        floorMan.SpawnRoom(starterRoom);
        floorMan.UpdatePlayerAndCameraPos();
    }
    void Update()
    {
        //IF PLAYER DIED, LOAD MENU
        if (GameObject.FindWithTag("Player") == null)
        {
            Invoke("LoadMenu", 1f);
            room = 0;
            floor = 1;
        }
        /* LOGIC FOR NEXZT FLOOR
        if (room >= 10)
        {
            room = 0;
            floor++;

            GameObject.FindWithTag("SceneSpawnPoint").transform.position = Vector2.zero; 
            Debug.Log("Floor Completed!");
        }*/ 
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
