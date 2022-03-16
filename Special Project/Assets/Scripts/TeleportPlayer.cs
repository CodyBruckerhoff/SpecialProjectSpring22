using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPlayer : MonoBehaviour
{
    public GameObject teleportTo;
    public GameObject location1;
    public GameObject location2;
    public moveWallButton moveWallButton;

    private void Start()
    {
        teleportTo = location1;
    }

    private void Update()
    {
        if (moveWallButton.isPressed)
        {
            teleportTo = location2;
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            collision.collider.transform.position = teleportTo.transform.position;
        }
    }
}
