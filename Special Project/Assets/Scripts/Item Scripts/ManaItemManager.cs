using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaItemManager : MonoBehaviour
{
    public float manaTotalIncrease;
    public float manaRegenIncrease;
    public float manaIncrease;

    public GameObject player;
    private CastSpell castSpell;

    public float speed;
    private float x, z;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        castSpell = player.GetComponent<CastSpell>();
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
            castSpell.currentMana += manaIncrease;
            castSpell.manaTotal += manaTotalIncrease;
            castSpell.manaRegen += manaRegenIncrease;

            // Then destroy

            Destroy(gameObject);
        }
    }
}
