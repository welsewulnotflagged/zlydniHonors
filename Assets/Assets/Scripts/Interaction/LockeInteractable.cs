using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockeInteractable : Interactable
{
    // public bool isHiding;
    public GameObject playerModel;

    // public float hideDuration = 3f;
    public GameObject lockerDoor; // The door of the locker
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

        if (cameraController)
        {
            if (!player.isHidden)
            {
                //  playerOriginalPosition = playerModel.transform.position;
                cameraController.Enable(player.gameObject);
                Debug.Log("Switching cameras");
                StartHiding();
            }
            else if (player.isHidden)
            {
                StopHiding(playerOriginalPosition);
            }
        }
    }

    void StartHiding()
    {
        player.isHidden = true;
        player.canMove = true;

        // Vector3 hidingCoord =  hidingSpot.position;

        //  playerModel.transform.position = hidingSpot.position;
        playerModel.SetActive(false);

        CameraController cameraNew = GetComponent<CameraController>();
        Debug.Log("" + player.isHidden);
    }

    void StopHiding(Vector3 OGposition)
    {
        player.isHidden = false;
        player.canMove = false;

        // playerModel.transform.position = OGposition;
        playerModel.SetActive(true);
        cameraController.Disable(player.gameObject);
        Debug.Log("" + player.isHidden);
    }

    public bool IsOccupied()
    {
        Debug.Log("Im hidden:" + player.isHidden);
        return player.isHidden;
    }
}