using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LockerCameraRotator : MonoBehaviour
{
    LockeInteractable lockerController;
    public float rotationSpeed = 50f;
    public CinemachineVirtualCamera lockerCamera;
    private Quaternion cameraRotation;
    float max = 20f;

    // Start is called before the first frame update
    void Start()
    {
        lockerController = GetComponent<LockeInteractable>();
        cameraRotation = lockerCamera.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        {
            RotateCamera();
        }
    }

    void RotateCamera()
    {
        float rotationAmount = Input.GetAxis("Horizontal") * rotationSpeed * 2 * Time.deltaTime;

    float clampedRotationAmount = Mathf.Clamp(rotationAmount, -max, max);

    lockerCamera.transform.Rotate(0f, clampedRotationAmount, 0f);

    //Debug.Log("I'm rotating");
    }
}
