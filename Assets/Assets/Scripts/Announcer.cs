using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Announcer : MonoBehaviour
{
    public DialogueAsset announcementAsset;
   public CameraController camera;
   private GameObject[] announcerObjects;

    void Start()
    {
        announcerObjects = GameObject.FindGameObjectsWithTag("Announcer");
    }

    private void OnTriggerEnter(Collider other)
    {
        DialogueController announce = FindObjectOfType<DialogueController>();
        announce.addDialogue(announcementAsset, camera);
        //camera.SwitchCamera();
        ColliderOff();
    }

    public void ColliderOff()
    {   
        foreach (GameObject announcer in announcerObjects)
        {   
            var announcerCollider = announcer.GetComponent<SphereCollider>();
            announcerCollider.enabled = false;
        } 
        //this.gameObject.SetActive(false);
    }
}
