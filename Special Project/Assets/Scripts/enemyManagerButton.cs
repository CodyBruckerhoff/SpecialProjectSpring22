using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyManagerButton : MonoBehaviour
{
    public GameObject enemySpawner;
    private EnemyManager enemyManager;

    private bool isPressed;

    void start()
    {
        isPressed = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Spell")
        {

            enemyManager = enemySpawner.GetComponent<EnemyManager>();
            enemyManager.canSpawn = false;

        }
    }
}
