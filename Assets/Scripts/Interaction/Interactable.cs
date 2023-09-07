using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {
    public InteractionType InteractionType = InteractionType.PUNCH;
    private Rigidbody _rigidbody;

    // Start is called before the first frame update
    void Start() {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() { }

    public void Interact(GameObject source) {
        Debug.Log("INTERACTED");
        switch (InteractionType) {
            case InteractionType.PUNCH:
                Debug.Log("PUNCHED");
                _rigidbody.AddForce(source.transform.TransformDirection(Vector3.forward) * 500);
                break;
        }
    }
}