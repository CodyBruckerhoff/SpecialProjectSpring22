using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPower : MonoBehaviour
{
    public float speed = 5f;
    private float x, z;
    public float HealthCurrent;
    [SerializeField] private float healthTotal;
    [SerializeField] private GameObject explosion;
    [SerializeField] private float XCoords;
    [SerializeField] private float YCoords;
    [SerializeField] private float ZCoords;
    public bool isActive;


    // Start is called before the first frame update
    void Start()
    {
        isActive = true;
        HealthCurrent = healthTotal;
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

    public void TakeDamage(float damage)
    {
        HealthCurrent -= damage;

        if (HealthCurrent <= 0)
        {
            isActive = false;
            Instantiate(explosion, new Vector3(XCoords, YCoords, ZCoords), Quaternion.identity);
            Destroy(gameObject);
        }
            
    }
}
