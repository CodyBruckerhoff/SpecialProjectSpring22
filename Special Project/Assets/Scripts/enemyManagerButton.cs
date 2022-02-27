using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyManagerButton : MonoBehaviour
{

    public GameObject walls;
    private MoveWalls moveWallScript;
    public GameObject enemySpawner;
    private EnemyManager enemyManager;

    public bool isPressed;

    void start()
    {
        isPressed = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Spell")
        {
            isPressed = true;
            moveWallScript = walls.GetComponent<MoveWalls>();
            moveWallScript.timer = 0;
            moveWallScript.buttonIsPressed = true;
            Debug.Log("Stop Spawning");
            enemyManager = enemySpawner.GetComponent<EnemyManager>();
            enemyManager.canSpawn = false;

        }
    }
}
