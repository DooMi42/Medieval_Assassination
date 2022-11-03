using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI1 : MonoBehaviour
{

    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;
    
    public EnemyVision vision;


    //kävely ympäriinsä
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //hyökkääminen
    public float timeBetweenAttacks;
    public float timeToCalm = 7f;
    public bool alreadyAttacked;

    //states
    public float attackRange, sightRange;
    public bool playerInAttackRange, playerInSightRange;

    private void Awake() 
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        //onko peluri -sihti- ja attack rangessa6
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);  

        if (!vision.angry && !playerInAttackRange) Patrolling(); 
        if (vision.angry && !playerInAttackRange) ChasePlayer(); 
        if (vision.angry && playerInAttackRange) AttackPlayer();
        if (vision.angry && !playerInAttackRange && !playerInSightRange) Invoke(nameof(Disengage), timeToCalm);
    }


    private void Disengage()
    {
        vision.angry = false;
    }
    private void Patrolling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //walkpointiin saavuttu
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        //valitse satunnainen piste kentässä
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
            Debug.DrawRay(walkPoint, -transform.up, Color.green);
        }

    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        transform.LookAt(player);

        if(!alreadyAttacked)
        {
            ///tähän miekalla lyömiskoodi




            ///
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }



}