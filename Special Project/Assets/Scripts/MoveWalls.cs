using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWalls : MonoBehaviour
{
    private float seconds = 10;
    public float timer;
    public Transform pointLocation;
    private Vector3 Point;
    private Vector3 Difference;
    private Vector3 start;
    private float percent;
    public bool buttonIsPressed;

    private void Start()
    {
        buttonIsPressed = false;   
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
        if (timer <= seconds && buttonIsPressed)
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
