using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject[] items;
    private float index;
    private GameObject selectItem;
    private Vector3 position;

    // Start is called before the first frame update
    void Start()
    {
        position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        index = Random.Range(0f, items.Length);

        Instantiate(items[(int)index], transform);
    }
}
