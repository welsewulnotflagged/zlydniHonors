using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{   
    public NavMeshAgent enemyAgent;
    public Transform playerPosition;
    public Transform pathHolder;

    public float speed;
    public float waitTime; 
    // Start is called before the first frame update
    
    private Transform[] Waypoints;
    public int CurrentWaypointIndex = 0;

    public void Start() {
        Waypoints = new Transform[pathHolder.transform.childCount];
        for (int i = 0; i < pathHolder.transform.childCount; i++)
        {
            Waypoints[i] = pathHolder.transform.GetChild(i);
        }
        enemyAgent = GetComponent<NavMeshAgent>();
        enemyAgent.SetDestination(Waypoints[0].position);
    }

    public void Update() {
        Debug.Log("DISTANCE "+Vector3.Distance(transform.position, Waypoints[CurrentWaypointIndex].position));
        if (Vector3.Distance(transform.position, Waypoints[CurrentWaypointIndex].position) <= 2.0f) {
            CurrentWaypointIndex = (CurrentWaypointIndex+1) % Waypoints.Length;
            enemyAgent.SetDestination(Waypoints[CurrentWaypointIndex].transform.position);    
        }
    }

    void OnDrawGizmos()
    {   Vector3 startPosition = pathHolder.GetChild(0).position;
        Vector3 previousPosition = startPosition;
        foreach (Transform waypoint in pathHolder) {
            Gizmos.DrawSphere(waypoint.position, 0.5f);
            Gizmos.DrawLine(previousPosition, waypoint.position);
            previousPosition = waypoint.position;
        }
        Gizmos.DrawLine(previousPosition, startPosition);
    }

}
