using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPower : MonoBehaviour
{
    public float speed = 5f;
    private float x, z;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        spinItem();
    }

    private void spinItem()
    {
        transform.Rotate(Vector3.up * speed * Time.deltaTime);
        transform.Rotate(Vector3.right * speed * Time.deltaTime);
        transform.Rotate(Vector3.forward * speed * Time.deltaTime);
        //transform.RotateAround(transform.position, transform.up, Time.deltaTime * speed);
    }
}
