using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloorTransitionUI : MonoBehaviour
{
    public List<Transform> gridLines = new List<Transform>();
    public Transform marker; 

    void Start()
    {
        StartCoroutine(IncrementMarker());
    }
    IEnumerator IncrementMarker()
    {
        yield return null;

        int floor = GameManager.floor;
        //Out of boudns
        if (floor >= gridLines.Count|| floor < 0)
        {
            Debug.Log("OUT OF BOUNDS!");
            yield break;
        } 
        //THIS SCENE IS ONLY CALLED WHEN FLOOR > 2

        marker.position = gridLines[floor-2].position;
         
        StartCoroutine(MoveToPosition(gridLines[floor-1]));
    } 

    IEnumerator MoveToPosition(Transform target)
    {
        yield return new WaitForSeconds(0.7f);
        float elapsedTime = 0f;
        float moveTime = 2f;

        while (elapsedTime < moveTime)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / moveTime);
            marker.position = Vector3.Lerp(marker.position, target.position, t);
            yield return null;
        }
        marker.position = target.position;
    } 
}
