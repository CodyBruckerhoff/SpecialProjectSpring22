using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItemManager : MonoBehaviour
{
    public float healthTotalIncrease;
    public float healthRegenIncrease;
    public float healthIncrease;
    public float armorIncrese;

    public GameObject player;
    private PlayerHealth playerHealth;

    public float speed;
    private float x, z;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
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
            playerHealth.playerHealthTotal += healthTotalIncrease;
            playerHealth.playerHealthRegen += healthRegenIncrease;
            playerHealth.playerHealthCurrent += healthIncrease;
            playerHealth.playerArmor += armorIncrese;


            // Then destroy

            Destroy(gameObject);
        }
    }
}
