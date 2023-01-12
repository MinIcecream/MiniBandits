using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using RoomInfo;

public class GameManager : MonoBehaviour
{
    public static int floor=0;
    public static int room=0;

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
    void Awake()
    {
        FloorManager floorMan = GameObject.FindWithTag("FloorManager").GetComponent<FloorManager>();

        RoomInfo.room starterRoom = new RoomInfo.room();
        starterRoom.roomType = roomTypes.starter;
         

        floorMan.SpawnRoom(starterRoom);
    }
    void Update()
    {
        //IF PLAYER DIED, LOAD MENU
        if (GameObject.FindWithTag("Player") == null)
        {
            Invoke("LoadMenu", 1f);
            room = 0;
            floor = 0;
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
