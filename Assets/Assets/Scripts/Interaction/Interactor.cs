using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour {
    public float InteractionRange = 5f;

    private GameObject _hitObject;
    private DialogueController _dialogueController;

    private void Start() {
        _dialogueController = FindObjectOfType<DialogueController>();
    }

    public void OnGUI() {
        if (_hitObject != null) {
            GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, 50, 20), _hitObject.name);
            GUI.Label(new Rect(Screen.width / 2, Screen.height / 2 + 20, 80, 20), "PRESS E");
        }
    }

    private void tryRaycast() {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, InteractionRange) && hit.collider.gameObject.GetComponent<Interactable>() != null) {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.green);
            _hitObject = hit.collider.gameObject;
        } else {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * InteractionRange, Color.red);
            _hitObject = null;
        }
    }

    void Update() {
        if (_dialogueController.HasActiveDialogue()) {
            return;
        }

        tryRaycast();

        if (Input.GetKeyDown(KeyCode.E) && _hitObject != null) {
            _hitObject.GetComponent<Interactable>().Interact(gameObject);
        }
    }
}