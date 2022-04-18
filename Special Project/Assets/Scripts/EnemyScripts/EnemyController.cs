using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player, gunPoint;

    public LayerMask whatIsGround, whatIsPlayer;

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

    //Animation Controller
    [SerializeField] private Animator animator;
    private bool isDead = false;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
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
        animator.SetBool("PlayerInSightRange", false);
        animator.SetBool("PlayerInAttackRange", false);
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
        animator.SetBool("PlayerInSightRange", true);
        animator.SetBool("PlayerInAttackRange", false);
        if (!isDead)
            agent.SetDestination(player.position);
    }

    private void Attacking()
    {
        animator.SetBool("PlayerInSightRange", true);
        animator.SetBool("PlayerInAttackRange", true);
        //Stop enemy movement

        if (!isDead)
            agent.SetDestination(transform.position);

        Vector3 targetPosition = new Vector3(player.position.x, this.transform.position.y, player.position.z);

        this.transform.LookAt(targetPosition);

    }

    private void CreatBullet()
    { 
        if (!alreadyAttacked)
        {
            // Attack code
            GameObject currentBullet = Instantiate(projectile, gunPoint.position, Quaternion.identity);
            Rigidbody rb = currentBullet.GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            //

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
        {
            isDead = true;
            Collider temp = GetComponent<Collider>();
            agent.enabled = !agent.enabled;
            temp.enabled = !temp.enabled;
            animator.SetTrigger("isDead");
            Invoke(nameof(DestroyEnemy), 3f);
        }
    }
    private void healthCheck()
    {

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
