using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpiderController : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public float health;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    //Animation
    private Animator animator;
    private bool isDead = false;

    private bool playOnce = true;
    private AudioSource walk;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        walk = GetComponent<AudioSource>();
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
        walk.Stop();
        playOnce = true;
        animator.SetBool("IsMoving", true);
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet && !isDead)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        //agent.speed = 3;
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void Chasing()
    {
        if (playOnce)
        {
            walk.Play();
            playOnce = false;
        }
        //agent.speed = 3;
        if (!isDead)
            agent.SetDestination(player.position);
    }

    private void Attacking()
    {
        //Stop enemy movement
        //agent.SetDestination(transform.position);


        Vector3 targetPosition = new Vector3(player.position.x, this.transform.position.y, player.position.z);

        this.transform.LookAt(targetPosition);

        if (!alreadyAttacked)
        {
            agent.speed = 10;

            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            animator.SetTrigger("Attack");
            collision.collider.gameObject.GetComponent<PlayerHealth>().TakeDamage(10f);
        }
    }

    private void ResetAttack()
    {
        agent.speed = 3;
        alreadyAttacked = false;
    }

    public void TakeDamage(float damage)
    {
        animator.SetTrigger("Hit");
        health -= damage;

        if (health <= 0)
        {
            walk.Stop();
            agent.Stop();
            isDead = true;
            animator.SetBool("IsDead", true);
            Invoke(nameof(DestroyEnemy), 3f);
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
