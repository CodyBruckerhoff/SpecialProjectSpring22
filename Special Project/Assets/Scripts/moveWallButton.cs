using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveWallButton : MonoBehaviour
{
    public GameObject walls;
    private MoveWalls moveWallScript;
    public bool isPressed;

    void start()
    {
        isPressed = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            moveWallScript = walls.GetComponent<MoveWalls>();
            moveWallScript.timer = 0;
            moveWallScript.buttonIsPressed = true;
            isPressed = true;
        }
    }
}
