using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject enemy;
    public Transform spawnPoint;
    public bool canSpawn;
    public float timer;

    // Start is called before the first frame update
    void Start()
    {
        canSpawn = true;
        StartCoroutine(spawnEnemy());
    }

    IEnumerator spawnEnemy()
    {
        while (canSpawn)
        {
            Instantiate(enemy, spawnPoint.position, Quaternion.identity);

            yield return new WaitForSeconds(timer);
        }
    }
}
