using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RoomInfo;

public class FloorManager : MonoBehaviour
{
    public int roomOffset;

    //Gamemanager initializes variable holding this floor's theme (dungeon, spider, graveyard,etc)
    roomThemes floorTheme = roomThemes.graveyard;

    //Where to spawn the next room
    public Transform roomSpawnPt;

    //Where player is spawned when they enter the next room
    public Transform playerSpawnPt;
     

    //Generate a list of 10 random level scriptableobjects 
    public List<Room> enemies = new List<Room>(); 

    void Awake()
    {
        Object[] everyEnemy = Resources.LoadAll("Rooms/"+floorTheme.ToString(), typeof(Room));
        List<Room> tempList = new List<Room>();

        foreach(Object r in everyEnemy)
        {
            tempList.Add((Room)r);
        }
         
        while (enemies.Count < 10)
        { 
            int ran = Random.Range(0, tempList.Count - 1);
            enemies.Add(tempList[ran]);
            tempList.RemoveAt(ran);
        }
    }
    public void SpawnRoom(room room)
    {
        if (room.roomType == roomTypes.normal)
        {
            GameObject newLevel = (GameObject)Instantiate(Resources.Load("RoomLayouts/" + floorTheme.ToString()), roomSpawnPt.position, Quaternion.identity);

            ProgressRoomManager man;
            man = newLevel.transform.Find("LevelManager").gameObject.GetComponent<ProgressRoomManager>();

            if (man == null)
            {
                Debug.Log("NO LEVEL MANAGER ATTACHED TO :" + newLevel);
            }
            else
            {
                playerSpawnPt = man.playerSpawnPt;
                if (enemies[0] != null)
                {
                    man.room = enemies[0];
                    enemies.RemoveAt(0);
                }
                else
                {
                    Debug.Log("NO MORE ROOMS TO ASSIGN THIS LEVELAMANGER!!!");
                }
            } 
        }
        else
        {
            Instantiate(Resources.Load("RoomLayouts/" + room.roomType.ToString()), roomSpawnPt.position, Quaternion.identity);

        } 
        roomSpawnPt.position = new Vector2(roomSpawnPt.position.x, roomSpawnPt.position.y + roomOffset);
    } 
    void Update()
    {
        if (Input.GetKeyDown("l"))
        {
            room[] rooms = RoomOptionGenerator.GenerateRoomOptions(1);
             
            SpawnRoom(rooms[0]);
        }
    }
    public void UpdatePlayerAndCameraPos()
    { 
        GameObject.FindWithTag("Player").transform.position = playerSpawnPt.position;

        GameObject.FindWithTag("CameraParent").transform.position = new Vector2(roomSpawnPt.position.x,roomSpawnPt.position.y-roomOffset);
    } 
}
