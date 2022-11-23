using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{

    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;    // kertoo navmeshille mikä on peluri ja mikä on maa 
    
    public EnemyVision vision;      // referenssi tötteröön vihollisen edessä (näkökenttä)

    public Transform[] patrolPoints;
    


    //kävely ympäriinsä
    public Vector3 walkPoint;   // kertoo navmesh jutulle missä walck point on
    bool walkPointSet;
    public float walkPointRange; // pois käytöstä mutta en jaksa kommentoida kaikkea pois 
    public int idleTime;    // kuinka kauan pahis pysyy paikallaan patrol pisteessä
    public bool reachedPoint;   // lippu onko päässyt pisteeseen, ei välttämättä tarpeellinen?
    public int current;     // nykyinen patrol piste index


    //hyökkääminen
    public float timeBetweenAttacks;
    public float timeToCalm = 7f;   // aika, jossa vihollinen menee chase tilasta takaisin patrol tilaan
    public bool alreadyAttacked;

    //states
    public float attackRange, sightRange;   // sight range on vihun ympärillä oleva alue, joka määrittää jatkaako vihollinen-
                                            // -jahtaamista vai meneekö takaisin patrol tilaan

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

        if (!vision.angry && !playerInAttackRange) Patrolling3();//Invoke(nameof(Patrolling3), idleTime);
        if (vision.angry && !playerInAttackRange) ChasePlayer(); 
        if (vision.angry && playerInAttackRange) AttackPlayer();
        if (vision.angry && !playerInAttackRange && !playerInSightRange) Invoke(nameof(Disengage), timeToCalm);
        
    }


    private void Disengage()
    {
        vision.angry = false;
    }
    private void Patrolling() //randomisti toimiva patrol homma, ei ihanteellinen mutta toimiva 0_0
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
    private void Patrolling2()      //for loopilla yritetty, en tiedä saako toimimaan
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

    private void Patrolling3()
    {
        // netistä otettu :d
        if (transform.position != patrolPoints[current].position)
        {
            walkPoint = patrolPoints[current].position;
            agent.SetDestination(walkPoint);
        } else {
            current = (current + 1) % patrolPoints.Length;
        }
    }

    private void nextPoint(int i)   // patrol2 käyttämä homma
    {
        if (i < 4)
        {
            i++;
        } else {
            i = 0;
        }
        walkPointSet = false;
    }


    private void SearchWalkPoint() // rando
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


