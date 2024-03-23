using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControllerStatesTypes : MonoBehaviour
{
    public NavMeshAgent agent;

    // public Transform player;
    public PlayerController _player;


    public LayerMask whatIsGround, whatIsPlayer;
    //  public LockeInteractable[] _lockers;

    //patrolling
    public Vector3 walkPoint;
    private bool _walkPointSet;
    public float walkPointRange;

    //attacking
    public float timeBetweenAttacks;
    private bool _alreadyAttacked;
    // public GameObject projectile;

    //States
    public float sightRange; // attackRange;

    public bool playerInSightRange, playerInAttackRange;

    // Start is called before the first frame update
    void Awake()
    {
        _player = FindFirstObjectByType<PlayerController>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_player.isHidden)
        {
            Debug.Log("player not hidden");
            //check range
            var position = transform.position;
            playerInSightRange = Physics.CheckSphere(position, sightRange, whatIsPlayer);

            if (!playerInSightRange && !playerInAttackRange) Patroling();
            if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        }
        else
        {
            Debug.Log("player hidden");

            playerInSightRange = false;
            Patroling();
        }
    }

    private void Patroling()
    {
        if (!_walkPointSet) SearchWalkPoint();
        if (_walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkpoint = transform.position - walkPoint;
        //walkpoint reached
        if (distanceToWalkpoint.magnitude < 1f)
            _walkPointSet = false;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(_player.gameObject.transform.position);
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            _walkPointSet = true;
    }
}