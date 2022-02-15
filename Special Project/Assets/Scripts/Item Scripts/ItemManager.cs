using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public float speed;
    private float x, z;
    // Start is called before the first frame update
    void Start()
    {
        // x = transform.Rotate.x;
        //z = transform.Rotate.z;
    }

    // Update is called once per frame
    void Update()
    {
        spinItem();
    }

    private void spinItem()
    {
        //transform.Rotate(Vector3.up * speed * Time.deltaTime);
        transform.RotateAround(transform.position, transform.up, Time.deltaTime * speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            // Do Item Effect


            // Then destroy

            Destroy(gameObject);
        }
    }
}