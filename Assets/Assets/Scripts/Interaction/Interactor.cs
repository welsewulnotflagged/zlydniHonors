using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour {
    public float InteractionRange = 5f;

    private Interactable _hitObject;
    private DialogueController _dialogueController;
    private PlayerController _playerController;

    private void Start() {
        _playerController = FindObjectOfType<PlayerController>();
        _dialogueController = FindObjectOfType<DialogueController>();
    }

    public void OnGUI() {
        if (_hitObject != null && !_dialogueController.HasActiveDialogue()) {
            GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, 50, 20), _hitObject.GetPrompt());
        }
    }

    private void tryRaycast() {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, InteractionRange) && hit.collider.gameObject.GetComponent<Interactable>() != null) {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.green);
            _hitObject = hit.collider.gameObject.GetComponent<Interactable>();
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
            _hitObject.GetComponent<Interactable>().Interact(_playerController.gameObject);
        }
    }
}