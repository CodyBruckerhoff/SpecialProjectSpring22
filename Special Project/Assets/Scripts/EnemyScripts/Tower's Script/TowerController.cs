using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    // Player and laser components
    public Transform player, gunPointL, gunPointR;
    private Transform gunPointActive;
    private bool GunPointLisActive;
    
    //Attacking
    public float timeBetweenAttacks;
    [SerializeField] private bool alreadyAttacked;
    public GameObject projectile;

    // Finding player elements
    public LayerMask whatIsGround, whatIsPlayer;
    RaycastHit hit;

    // Projectile Info
    private Spell spell;

    // Pivot point
    [SerializeField] private Transform pivot;


    //States
    public float attackRange;
    public bool playerInAttackRange;
    [SerializeField] private CheckPower checkPower;

    // Start is called before the first frame update
    void Start()
    {

        gunPointActive = gunPointL;
        GunPointLisActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (checkPower.allTurretsPowered && playerInAttackRange) Attacking();
    }

    private void Attacking()
    {
        Vector3 targetPosition = new Vector3(player.position.x, this.transform.position.y, player.position.z);

        pivot.transform.LookAt(player);

        Debug.DrawLine(gunPointActive.transform.position, player.transform.position);

        if (!alreadyAttacked)
        {
            LaserAttack();
        }
    }
    private void LaserAttack()
    {
        // Attack code
        GameObject currentBullet = Instantiate(projectile, gunPointActive.position, Quaternion.Euler(0, 0, 0));
        Debug.Log("Create laser");
        Rigidbody rb = currentBullet.GetComponent<Rigidbody>();
        currentBullet.transform.LookAt(player);
        rb.AddForce(currentBullet.transform.forward * 150f, ForceMode.Impulse);

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

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void DestroyObject(GameObject projectile)
    {
        var destroyTime = 5;
        Destroy(projectile, destroyTime);
    }

}
