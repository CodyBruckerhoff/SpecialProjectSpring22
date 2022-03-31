using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    public Spell spell;
    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.collider.name);
        if (collision.collider.CompareTag("Enemy"))
        {
            Debug.Log("with enemy");
            collision.collider.gameObject.GetComponent<EnemyController>().TakeDamage(spell.damage);
            DestroyProjectile();
        }
        else if (collision.collider.CompareTag("Player"))
        {
            collision.collider.gameObject.GetComponent<PlayerHealth>().TakeDamage(spell.damage);
            DestroyProjectile();

        }
        else if (collision.collider.CompareTag("SpiderEnemy"))
        {
            collision.collider.gameObject.GetComponent<SpiderController>().TakeDamage(spell.damage);
            DestroyProjectile();

        }
        else if (collision.collider.CompareTag("Boss"))
        {
            collision.collider.gameObject.GetComponent<BossController>().TakeDamage(spell.damage);
            DestroyProjectile();
        }
        else if (collision.collider.CompareTag("TowerPower"))
        {
            collision.collider.gameObject.GetComponent<TowerPower>().TakeDamage(spell.damage);
            DestroyProjectile();
        }

    }

    private void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
