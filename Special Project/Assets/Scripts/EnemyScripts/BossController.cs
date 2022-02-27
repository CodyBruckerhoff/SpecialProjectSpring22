using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    // Gameplay Objects
    public UnityEngine.AI.NavMeshAgent agent;

    public Transform player, gunPointL, gunPointR;
    private Transform gunPointActive;
    private bool GunPointLisActive;

    public LayerMask whatIsGround, whatIsPlayer;
    RaycastHit hit;

    private Spell spell;
    private Transform head;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;
    public float health;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        //player = GameObject.Find("Player").transform;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        gunPointActive = gunPointL;
        GunPointLisActive = true;
        head = GameObject.Find("Mech Head").transform;
        //spell = projectile.GetComponent<CollisionDetection>().spell;
    }

    private void Update()
    {
        // Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) Chasing();
        if (playerInSightRange && playerInAttackRange) Attacking();

    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void Chasing()
    {
        agent.SetDestination(player.position);
        head.transform.LookAt(player);
    }

    private void Attacking()
    {
        //Stop enemy movement
        agent.SetDestination(transform.position);

        Vector3 targetPosition = new Vector3(player.position.x, this.transform.position.y, player.position.z);

        head.transform.LookAt(player);

        this.transform.LookAt(targetPosition);

        Debug.DrawLine(gunPointActive.transform.position, player.transform.position);

        if (!alreadyAttacked)
        {
            // Attack code
            GameObject currentBullet = Instantiate(projectile, gunPointActive.position, Quaternion.Euler(90, 0, 0));
            Rigidbody rb = currentBullet.GetComponent<Rigidbody>();
            currentBullet.transform.LookAt(player);
            rb.AddForce(currentBullet.transform.forward * 500f, ForceMode.Impulse);

        

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

            DestroyObject(currentBullet);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
            Invoke(nameof(DestroyEnemy), .5f);
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
