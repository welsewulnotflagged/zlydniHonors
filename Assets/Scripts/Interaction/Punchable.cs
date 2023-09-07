using System;
using UnityEngine;

public class Punchable : MonoBehaviour, Interactable {
    public float PunchStrength = 500f;

    private Rigidbody _rigidbody;


    public void Start() {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Interact(GameObject source) {
        _rigidbody.AddForce(source.transform.TransformDirection(Vector3.forward) * PunchStrength);
    }
}