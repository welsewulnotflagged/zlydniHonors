using System;
using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public int currentCameraIndex = 0;
    public CinemachineVirtualCamera[] cameras;

    public void Start() {
        cameras = GetComponentsInChildren<CinemachineVirtualCamera>();
    }

    public void SwitchCamera() {
        if (CanSwitch()) {
            var prevIndex = currentCameraIndex;
            currentCameraIndex = (currentCameraIndex + 1) % cameras.Length;
            cameras[prevIndex].Priority = 0;
            cameras[currentCameraIndex].Priority = 2;
        }
    }

    public bool CanSwitch() {
        return cameras.Length > 1;
    }

    public void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            Enable(other.gameObject);
        }
    }

    public bool isEnabled() {
        return cameras[currentCameraIndex].Priority > 0;
    }

    public void Enable(GameObject other) {
        cameras[currentCameraIndex].Priority = 2;
        var playerController = other.gameObject.GetComponent<PlayerContoller>();
        if (playerController) {
            playerController.currentCameraController = this;
        }
    }

    public void Disable(GameObject other) {
        foreach (var camera in cameras) {
            camera.Priority = 0;
        }

        other.gameObject.GetComponent<PlayerContoller>().currentCameraController = null;
    }

    public void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            Disable(other.gameObject);
        }
    }
}