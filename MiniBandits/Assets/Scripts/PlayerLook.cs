using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    //public Transform playerBody;
    public Joystick joystick;
    public GetClosestEnemyPosition enemyMan;

    void FixedUpdate()
    { 
        if(joystick.input == Vector2.zero)
        {
            return;
        }
        //Get the Screen positions of the object
        Vector2 positionOnScreen = (transform.position);

        //Get the Screen position of the mouse
        Vector2 mouseOnScreen = enemyMan.GetClosestEnemyPos();

        //Get the angle between the points
        float angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);

        //Ta Daaa
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle - 180)); 
    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

    public void Shake(int min, int max)
    {
        float random = Random.Range(min, max);
        transform.Rotate(new Vector3(0f, 0f, random));
    }
}
