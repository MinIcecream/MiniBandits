using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class RoomCreator : MonoBehaviour
{
    public EnemySpawnConfig room;

#if UNITY_EDITOR
    public void SaveRoom()
    {
    Debug.Log("DJ");
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Enemy");

        if (room != null)
        {
            EditorUtility.SetDirty(room);
            foreach (GameObject obj in objs)
            {
                room.AddEnemy(obj.GetComponent<EnemyAI>().name, obj.transform.position);
            }
        }
    }
}
[CustomEditor(typeof(RoomCreator))]
public class RoomCreatorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        RoomCreator creator = (RoomCreator)target;
        if (GUILayout.Button("Save Room"))
        {
            creator.SaveRoom();
        }
    }
#endif
}
