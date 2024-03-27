using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public float openAngle = 90f; // Angle to open the door
    private Quaternion closedRotation; // Initial rotation of the door
    private Quaternion openRotation; // Rotation when the door is open
    private int opens = 0; // Number of times the door opens
    private int closes = 0; // Number of times the door closes

    private bool playerInside = false; // Track if player is inside

    private void Start()
    {
        // Store initial and open rotations
        closedRotation = transform.rotation;
        openRotation = Quaternion.Euler(0, openAngle, 0);
    }

    private void Update()
    {
        // Check for manual door opening (optional)
        if (playerInside && Input.GetKeyDown(KeyCode.E))
        {
            ToggleDoor();
        }
    }

    private void ToggleDoor()
    {
        // Toggle door state
        if (transform.rotation == closedRotation)
        {
            OpenDoor();
        }
        else
        {
            CloseDoor();
        }
    }

    private void OpenDoor()
    {
        // Rotate the door to open position
        transform.rotation = openRotation;
        opens++;
        Debug.Log("Door opened. Opens: " + opens + ", Closes: " + closes);
    }

    private void CloseDoor()
    {
        // Rotate the door back to closed position
        transform.rotation = closedRotation;
        closes++;
        Debug.Log("Door closed. Opens: " + opens + ", Closes: " + closes);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger is the player
        if (other.CompareTag("Player"))
        {
            playerInside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the object exiting the trigger is the player
        if (other.CompareTag("Player"))
        {
            playerInside = false;
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        // If door collides with anything, freeze its position and rotation
        GetComponent<Rigidbody>().isKinematic = true;
    }
    
    private void OnCollisionExit(Collision collision)
    {
        // If door stops colliding, allow physics to affect it again
        GetComponent<Rigidbody>().isKinematic = false;
    }
}


