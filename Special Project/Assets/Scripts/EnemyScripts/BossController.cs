using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{
    // Gameplay Objects
    public UnityEngine.AI.NavMeshAgent agent;

    // Player and laser components
    public Transform player, gunPointL, gunPointR;
    private Transform gunPointActive;
    private bool GunPointLisActive;

    // Missile components
    public GameObject missle;
    public Transform missleBay;

    public LayerMask whatIsGround, whatIsPlayer;
    RaycastHit hit;

    private Spell spell;
    private Transform head;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks, timeBetweenMissleAttacks;
    bool alreadyAttacked, alreadyMissleAttack;
    public GameObject projectile;
    public GameObject rocketProjectile;

    //UI and Health elements
    [SerializeField] private Slider slider;
    [SerializeField] private float healthTotal;
    private float health;

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
        health = healthTotal;
    }

    private void Update()
    {
        // Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) Chasing();
        if (playerInSightRange && playerInAttackRange) Attacking();

        slider.value = (health / healthTotal);
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
            LaserAttack();
            if (health <= (healthTotal/2) && !alreadyMissleAttack)
            {
                MissleAttack();
            }
        }
    }

    private void LaserAttack()
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
    private void MissleAttack()
    {
        Instantiate(rocketProjectile, missleBay.position, Quaternion.Euler(0, 90, 0));
        alreadyMissleAttack = true;
        Invoke(nameof(ResetRocketAttack), timeBetweenMissleAttacks);
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
    private void ResetRocketAttack()
    {
        alreadyMissleAttack = false;
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

    public static GameObject[] GetDontDestroyOnLoadObjects()
    {
        GameObject temp = null;
        try
        {
            temp = new GameObject();
            Object.DontDestroyOnLoad(temp);
            UnityEngine.SceneManagement.Scene dontDestroyOnLoad = temp.scene;
            Object.DestroyImmediate(temp);
            temp = null;

            return dontDestroyOnLoad.GetRootGameObjects();
        }
        finally
        {
            if (temp != null)
                Object.DestroyImmediate(temp);
        }
    }
}
