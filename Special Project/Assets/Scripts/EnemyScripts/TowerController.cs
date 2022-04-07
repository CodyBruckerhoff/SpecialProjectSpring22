using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    // Player and laser components
    public Transform player, gunPointL, gunPointR;
    private Transform gunPointActive;
    private bool GunPointLisActive;

    public LayerMask whatIsGround, whatIsPlayer;
    RaycastHit hit;

    private Spell spell;
    [SerializeField]private Transform head;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;
    [SerializeField] private float healthTotal;
    private float health;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        gunPointActive = gunPointL;
        GunPointLisActive = true;
        health = healthTotal;
        //sceneChanger = DontDestroyOnLoadObjects[0].GetComponent<SceneChanger>();
    }

    private void Update()
    {
        // Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (playerInSightRange && !playerInAttackRange) Chasing();
        if (playerInSightRange && playerInAttackRange) Attacking();

    }

    private void Chasing()
    {
        head.transform.LookAt(player);
    }

    private void Attacking()
    {

        Vector3 targetPosition = new Vector3(player.position.x, this.transform.position.y, player.position.z);

        head.transform.LookAt(player);

        //this.transform.LookAt(targetPosition);

        Debug.DrawLine(gunPointActive.transform.position, player.transform.position);

        if (!alreadyAttacked)
        {
            LaserAttack();
        }
    }

    private void LaserAttack()
    {
        // Attack code
        GameObject currentBullet = Instantiate(projectile, gunPointActive.position, Quaternion.Euler(90, 0, 0));
        Rigidbody rb = currentBullet.GetComponent<Rigidbody>();
        currentBullet.transform.LookAt(player);
        rb.AddForce(currentBullet.transform.forward * 100f, ForceMode.Impulse);

        //Change laser position
        if (GunPointLisActive)
        {
            gunPointActive = gunPointR;
            GunPointLisActive = false;
        }
        else
        {
            gunPointActive = gunPointL;
            GunPointLisActive = true;
        }

        alreadyAttacked = true;
        Invoke(nameof(ResetAttack), timeBetweenAttacks);

        //DestroyObject(currentBullet);
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
    public void TakeDamage(float damage)
    {
        health -= damage;



        if (health <= 0)
        {
            Invoke(nameof(DestroyEnemy), .5f);
        }
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void DestroyObject(GameObject projectile)
    {
        var destroyTime = 5;
        Destroy(projectile, destroyTime);
    }
}
