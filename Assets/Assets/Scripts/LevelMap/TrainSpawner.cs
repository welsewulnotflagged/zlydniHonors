using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainSpawner : MonoBehaviour {
    public Transform[] waypoints;
    public Transform startMarker;
    public Transform endMarker;
    public float speed = 1.0F;
    
    private int currentWaypoint;
    // Time when the movement started.
    private float startTime;
    // Total distance between the markers.
    private float journeyLength;
    private float timePassed;


    void Start() {
        // Keep a note of the time the movement started.
        startTime = Time.time;

        // Calculate the journey length.
        journeyLength = Vector3.Distance(startMarker.position, endMarker.position);
    }

    // Follows the target position like with a spring
    void Update() {
        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypoint].position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, waypoints[currentWaypoint].position) < 0.1f) {
            if (currentWaypoint == waypoints.Length - 1) {
                transform.position = waypoints[0].position;
                currentWaypoint = 0;
            } else currentWaypoint++;
        }
    }
}