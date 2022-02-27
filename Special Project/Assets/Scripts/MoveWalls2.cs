using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWalls2 : MonoBehaviour
{
    private float seconds = 10;
    public float timer;
    public Transform pointLocation;
    private Vector3 Point;
    private Vector3 Difference;
    private Vector3 start;
    private float percent;
    public enemyManagerButton enemyManagerButton1;
    public enemyManagerButton enemyManagerButton2;
    public enemyManagerButton enemyManagerButton3;
    public enemyManagerButton enemyManagerButton4;

    private void Start()
    {
        start = transform.position;
        Point = new Vector3(pointLocation.position.x, pointLocation.position.y, pointLocation.position.z);
        Difference = Point - start;
    }

    private void Update()
    {
        moveWall();
    }

    public void moveWall()
    {
        if (timer <= seconds && enemyManagerButton1.isPressed && enemyManagerButton2.isPressed && enemyManagerButton3.isPressed && enemyManagerButton4.isPressed)
        {
            // basic timer
            timer += Time.deltaTime;
            // percent is a 0-1 float showing the percentage of time that has passed on our timer!
            percent = timer / seconds;
            // multiply the percentage to the difference of our two positions
            // and add to the start
            transform.position = start + Difference * percent;
        }
    }
}
