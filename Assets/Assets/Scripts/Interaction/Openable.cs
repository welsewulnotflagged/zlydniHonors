using System;
using UnityEngine;

public class Openable : Interactable {
    public bool opened = false;
    public GameObject doorHandle;
    public float speed = 5f;

    public void Update() {
        var targetTransform = doorHandle.transform;
        Vector3 currentRot = targetTransform.localEulerAngles;
        if (opened) {
            if (currentRot.y < 90) {
                targetTransform.localEulerAngles = Vector3.Lerp(currentRot, new Vector3(currentRot.x, 90, currentRot.z), speed * Time.deltaTime);
            }
        } else {
            if (currentRot.y > 0) {
                targetTransform.localEulerAngles = Vector3.Lerp(currentRot, new Vector3(currentRot.x, 0, currentRot.z), speed * Time.deltaTime);
            }
        }
    }

    protected override void OnInteract(GameObject source) {
        opened = !opened;
        Debug.Log((source.transform.position - transform.position).normalized);
    }
}