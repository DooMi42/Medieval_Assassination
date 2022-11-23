using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{

    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;
    
    public EnemyVision vision;

    public GameObject[] patrolPoints;
    


    //kävely ympäriinsä
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    public int idleTime;
    public bool reachedPoint;


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
        
        //onko peluri -sihti- ja attack rangessa
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);  

        if (!vision.angry && !playerInAttackRange) Invoke(nameof(Patrolling2), idleTime);
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
        if (distanceToWalkPoint.magnitude < 1.5f)
        {
            walkPointSet = false;
            Debug.Log("Walkpointiin saavuttu.");
        }
    
    }
    private void Patrolling2()
    {
        for (int i = 0; i < patrolPoints.Length; i++)
        {   
         if (!walkPointSet)
            {
            Debug.Log(i);
 
            walkPoint = new Vector3(patrolPoints[i].transform.position.x, patrolPoints[i].transform.position.y, patrolPoints[i].transform.position.z);

            agent.SetDestination(walkPoint);

            Vector3 distanceToWalkPoint = transform.position - walkPoint;

                if (distanceToWalkPoint.magnitude < 1.5f)
                {
                Debug.Log("Walkpointiin saavuttu.");
                nextPoint(i);
                }
            }
        }
    }

    private void nextPoint(int i)
    {
        if (i < 4)
        {
            i++;
        } else {
            i = 0;
        }
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


