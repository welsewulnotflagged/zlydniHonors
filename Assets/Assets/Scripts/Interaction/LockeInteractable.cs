using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockeInteractable : Interactable
{
    public bool isHiding;
    public GameObject playerModel;
   // public float hideDuration = 3f;
    public GameObject lockerDoor;  // The door of the locker
    public Transform hidingSpot;
    public PlayerController player;
    Vector3 playerOriginalPosition;


    public CameraController cameraController;

    void Start() 
    {

    }
    protected override void OnInteract(GameObject source)
    {   
        var player = FindObjectOfType<PlayerController>();
       // cameraController = GetComponentInChildren<CameraController>();
       
        if (cameraController){
            if (!isHiding) {
        playerOriginalPosition = playerModel.transform.position;
        cameraController.Enable(player.gameObject);
        Debug.Log("Switching cameras");
        StartHiding();
        
            } else if (isHiding)
            {
                StopHiding(playerOriginalPosition);
            }
        }
    }

    void StartHiding()
    {
        
        isHiding = true;
        player.canMove = true;

       // Vector3 hidingCoord =  hidingSpot.position;

        playerModel.transform.position = hidingSpot.position;
        Debug.Log(playerModel.transform.position);

        CameraController cameraNew = GetComponent<CameraController>();
       
    }

  /*  IEnumerator HideTimer(Vector3 OGposition)
    {
        yield return new WaitForSeconds(hideDuration);

        // Stop hiding
        StopHiding(OGposition);
    } */

    void StopHiding(Vector3 OGposition)
    {

        isHiding = false;
        player.canMove = false;

        playerModel.transform.position = OGposition;
        cameraController.Disable(player.gameObject);
    }

    
}
