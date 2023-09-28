using UnityEngine;

public class Punchable : Interactable {
    public float PunchStrength = 500f;

    private Rigidbody _rigidbody;


    public void Start() {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public override void Interact(GameObject source) {
        _rigidbody.AddForce(source.transform.TransformDirection(Vector3.forward) * PunchStrength);
    }
}