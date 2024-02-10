using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {
    public Transform pathHolder;
    public int DetectionRadius;
    [Range(0, 360)] public float DetectionAngle;
    public LayerMask targetMask;


    private Transform[] Waypoints;
    private NavMeshAgent _navMeshAgent;
    private PlayerController _playerController;
    private int CurrentWaypointIndex = 0;
    private bool chasingPlayer;
    private bool canSeePlayer;

    public void Start() {
        _playerController = FindObjectOfType<PlayerController>();
        _navMeshAgent = GetComponent<NavMeshAgent>();

        if (pathHolder != null) {
            Waypoints = new Transform[pathHolder.transform.childCount];
            for (int i = 0; i < pathHolder.transform.childCount; i++) {
                Waypoints[i] = pathHolder.transform.GetChild(i);
            }

            _navMeshAgent.SetDestination(Waypoints[0].position);
        }

        StartCoroutine(FOVRoutine());
    }

    public void Update() {
        if (canSeePlayer) {
            Debug.Log("im seeing");
            _navMeshAgent.SetDestination(_playerController.transform.position);
            chasingPlayer = true;
        } else {
            if (chasingPlayer) {
                Debug.Log("im not seeing");
               _navMeshAgent.SetDestination(Waypoints[CurrentWaypointIndex].transform.position);
                chasingPlayer = false;
            }
            if (Vector3.Distance(transform.position, Waypoints[CurrentWaypointIndex].position) <= 2.0f) {
                CurrentWaypointIndex = (CurrentWaypointIndex + 1) % Waypoints.Length;
                _navMeshAgent.SetDestination(Waypoints[CurrentWaypointIndex].transform.position);
            }
        }
    }

    void OnDrawGizmos() {
        Vector3 startPosition = pathHolder.GetChild(0).position;
        Vector3 previousPosition = startPosition;
        foreach (Transform waypoint in pathHolder) {
            Gizmos.DrawSphere(waypoint.position, 0.5f);
            Gizmos.DrawLine(previousPosition, waypoint.position);
            previousPosition = waypoint.position;
        }

       // Gizmos.DrawLine(transform.position, _playerController.transform.position);
       // Gizmos.DrawSphere(_playerController.transform.position, 1f);
        Gizmos.DrawLine(previousPosition, startPosition);
    }

    private IEnumerator FOVRoutine() {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true) {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck() {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, DetectionRadius, targetMask);
        if (rangeChecks.Length != 0) {
          //  Debug.Log("PLAYER IN RANGE");
            Debug.Log(rangeChecks.Length);

            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < DetectionAngle / 2) {
                float distanceToTarget = Vector3.Distance(transform.position, target.position) +2 ;
                Debug.Log("PLAYED SPOTTED AT "+distanceToTarget);

                if (Physics.Raycast(transform.position, directionToTarget, distanceToTarget, targetMask)) {
                    canSeePlayer = true;
                } else {
                    canSeePlayer = false;
                }
            } else {
                canSeePlayer = false;
            }
        } else if (canSeePlayer) {
            canSeePlayer = false;
        }
    }

    public Vector3 GetPlayerPosition() {
        Debug.Log(_playerController.transform.position);
        return _playerController.transform.position;
    }

    public bool CanSeePlayer() {
        return canSeePlayer;
    }
}
