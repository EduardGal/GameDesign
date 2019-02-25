using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour {

    [SerializeField] float patrolSpeed = 1.0f;
    [SerializeField] float patrolWaitTime = 1.0f;
    [SerializeField] float chaseSpeed = 1.5f;
    [SerializeField] float chaseWaitTime = 5.0f;
    [SerializeField] float stoppingDistanceFromPlayers = 7.0f;
    [SerializeField] Transform[] patrolWayPoints;

    private EnemySenses enemySenses;
    private NavMeshAgent navMeshAgent;
    private Transform playerOne;
    private Transform playerTwo;
    private PlayersLastLocation playersLastLocation;

    private float patrolTimer;
    private float chaseTimer;
    private int currentWaypointIndex;

    private void Awake()
    {
        enemySenses = GetComponent<EnemySenses>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        playerOne = GameObject.FindGameObjectWithTag("PlayerOne").transform;
        playerTwo = GameObject.FindGameObjectWithTag("PlayerTwo").transform;
        playersLastLocation = GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayersLastLocation>();
    }

    private void Update()
    {
        if (enemySenses.isPlayerOneInSight || enemySenses.isPlayerTwoInSight)
        {
            AnalysePlayer();
        }
        else if(enemySenses.playerOneLastKnownLocation != playersLastLocation.resetPosition || enemySenses.playerTwoLastKnownLocation != playersLastLocation.resetPosition)
        {
            ChasePlayer();
        }
        else
        {
            PatrolWaypoints();
        }
            
    }

    void AnalysePlayer()
    {
        // EnemyAnalyse script should trigger - if the player runs out of range, the enemy should run towards their last known location before going back to patrolling
        
    }

    void ChasePlayer()
    {
        Vector3 distanceToPlayerOne = enemySenses.playerOneLastKnownLocation - transform.position;
        Vector3 distanceToPlayerTwo = enemySenses.playerTwoLastKnownLocation - transform.position;

        //TODO: ensure that the enemy runs towards the closest player to their current location
        if (distanceToPlayerOne.sqrMagnitude > 4.0f)
        {
            navMeshAgent.destination = enemySenses.playerOneLastKnownLocation;
            navMeshAgent.stoppingDistance = stoppingDistanceFromPlayers;

        }
        else if (distanceToPlayerTwo.sqrMagnitude > 4.0f)
        {
            navMeshAgent.destination = enemySenses.playerTwoLastKnownLocation;
        }

        //TODO: tinker with the chaseSpeed - perhaps slow the speed when the enemy is analysing the player to allow the player to escape in time
        navMeshAgent.speed = chaseSpeed;

        if(navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
        {
            chaseTimer += Time.deltaTime;

            if(chaseTimer > chaseWaitTime)
            {
                playersLastLocation.playerOnePosition = playersLastLocation.resetPosition;
                playersLastLocation.playerTwoPosition = playersLastLocation.resetPosition;
                enemySenses.playerOneLastKnownLocation = playersLastLocation.resetPosition;
                enemySenses.playerTwoLastKnownLocation = playersLastLocation.resetPosition;
                chaseTimer = 0.0f;
            }
        }
        else
        {
            chaseTimer = 0.0f;
        }

    }
    //If the player escapes the enemy, they should continue to patrol
    void PatrolWaypoints()
    {
        navMeshAgent.speed = patrolSpeed;

        if(navMeshAgent.destination == playersLastLocation.resetPosition || navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
        {
            patrolTimer += Time.deltaTime;

            if(patrolTimer >= patrolWaitTime)
            {
                if (currentWaypointIndex == patrolWayPoints.Length - 1)
                    currentWaypointIndex = 0;
                else
                    currentWaypointIndex++;

                patrolTimer = 0.0f;
            }
        }
        else
        {
            patrolTimer = 0.0f;
        }

        navMeshAgent.destination = patrolWayPoints[currentWaypointIndex].position;
        navMeshAgent.baseOffset = 0.25f;
    }

}
