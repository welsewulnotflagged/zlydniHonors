using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingMechanic : MonoBehaviour
{
    public float hideDuration = 3f;
    public GameObject lockerDoor;  // The door of the locker
    public Transform hidingSpot;   // The position where the player should be placed inside the locker
    private PlayerController player;
    public GameObject playerModel;

    private bool isHiding = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isHiding)
        {
            TryHideInLocker();
        }
    }

    void TryHideInLocker()
    {
        if (Input.GetKey(KeyCode.E)) {
        // Raycast to check if the player is in front of the locker
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 2f))
        {
            if (hit.collider.gameObject == lockerDoor)
            {   Debug.Log("Pressed E");
                StartHiding();
            }
        }
        }
    }

    void StartHiding()
    {
        
        isHiding = true;
        player = GetComponent<PlayerController>();
        player.canMove = true;

        Vector3 playerOriginalPosition = playerModel.transform.position;

        playerModel.transform.position = hidingSpot.position;
        CameraController cameraNew = GetComponent<CameraController>();


        // Disable player controls or set them to a "hidden" state
        // For example: Disable player movement, hide the player's model, and adjust the camera position
        
        // Play any hiding animation if necessary

        StartCoroutine(HideTimer(playerOriginalPosition));
    }

    IEnumerator HideTimer(Vector3 OGposition)
    {
        yield return new WaitForSeconds(hideDuration);

        // Stop hiding
        StopHiding(OGposition);
    }

    void StopHiding(Vector3 OGposition)
    {
        isHiding = false;
        player = GetComponent<PlayerController>();
        player.canMove = false;

        playerModel.transform.position = OGposition;
        // Re-enable player controls and reset the player's position and camera to their original state
    }
}

