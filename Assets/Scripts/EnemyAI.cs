using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    /*
 kommentoitu pois koska ray on tulevaisuus
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;    // kertoo navmeshille mikä on peluri ja mikä on maa 
    
    public EnemyVision vision;      // referenssi tötteröön vihollisen edessä (näkökenttä)

    public Transform[] patrolPoints;

    public Vector3 pointLocation;
    


    //kävely ympäriinsä
    public Vector3 walkPoint;   // kertoo navmesh jutulle missä walck point on
    bool walkPointSet;
    public int idleTime;    // kuinka kauan pahis pysyy paikallaan patrol pisteessä
    public int current;     // nykyinen patrol piste index
    public bool travelDone = false;     // onko pahis pisteen luona?

    //hyökkääminen
    public float timeBetweenAttacks;
    public int timeToCalm = 7;   // aika, jossa vihollinen menee chase tilasta takaisin patrol tilaan
    public bool alreadyAttacked;

    //states
    public float attackRange, sightRange;   // sight range on vihun ympärillä oleva alue, joka määrittää jatkaako vihollinen-
                                            // -jahtaamista vai meneekö takaisin patrol tilaan

    public bool playerInAttackRange, playerInSightRange;

    

    private void Awake() 
    {
        player = GameObject.Find("PlayerModel").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        Ray ray = new Ray (transform.position, transform.forward);
        //onko peluri -sihti- ja attack rangessa
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);  
        

        if (!vision.angry && !playerInAttackRange) Patrolling();//Invoke(nameof(Patrolling3), idleTime);
        if (vision.angry && !playerInAttackRange) ChasePlayer(); 
        if (vision.angry && playerInAttackRange) AttackPlayer();
        if (vision.angry && !playerInAttackRange && !playerInSightRange) Invoke(nameof(Disengage), timeToCalm);
        
    }


    private void Disengage()
    {
        //kysyy sight rangea toista kertaa että ei lähde muualle kun pelaaja ihan edessä
        if (!playerInSightRange)
        {
        vision.angry = false;
        //travelDone = true;
        }
    }

    private void Patrolling()
    {
        // netistä otettu :d
        pointLocation = (patrolPoints[current].position);
        walkPoint = new Vector3(pointLocation.x, pointLocation.y, pointLocation.z);

        // dist tehty koska tötterö siirtää vihollisen keskipistettä.
        float dist = Vector3.Distance(pointLocation, transform.position);

        //Debug.Log(dist);

        //(Jos ei vielä mennyt seuraavaan pisteeseen)
        if (travelDone == false){ 
            
            if   (dist > 1.4f)
            {
                agent.SetDestination(walkPoint);

            } else {

                // tämä hallinnoi mihin pisteeseen pelaaja menee, menee viimeisestä pisteestä seuraavaan
                if (current < patrolPoints.Length - 1) {
                    current++;
                    Debug.Log("current on nyt " + current);
                    travelDone = true;
               } else {
                    current = 0;
                    Debug.Log("current asetettu 0");
                    travelDone = true;
                }
            }
            // jos on saavuttu 'seuraavaan' pisteeseen
            //-> pysyy paikallaan idletimen verran
        } else {
            Invoke(nameof(TravellingTime), idleTime);
        }
    }

    private void TravellingTime()
    {
        // mene seuraavaan pisteeseen
        travelDone = false;
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
*/
}


