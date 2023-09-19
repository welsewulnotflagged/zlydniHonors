using System;
using UnityEngine;

public class PlayerContoller : MonoBehaviour {
    public float speed = 5f;
    public float turningSpeed = 180f;
    private CharacterController _characterController;
    public CameraController currentCameraController;
    public float jumpHeight = 15f; 
    private float y_velocity; 

    // Start is called before the first frame update
    void Start() {
        _characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update() {
        tryMove();
        TrySwitchCamera();
    }

    private void TrySwitchCamera() {
        if (Input.GetKeyUp(KeyCode.X)) {
            if (currentCameraController != null && currentCameraController.CanSwitch()) {
                Debug.Log("TRY SWITCH CAMERA");
                currentCameraController.SwitchCamera();
            }
        }
    }


    private void tryMove() {
        Vector3 direction;
        

        transform.Rotate(0, Input.GetAxis("Horizontal") * Time.deltaTime * turningSpeed, 0);
        direction = transform.forward * Input.GetAxis("Vertical") * speed;
        
            if (Input.GetKeyDown(KeyCode.Space))
        {   
            Vector3 velocity = direction * speed;
            Debug.Log("SPACE");
            velocity.y = y_velocity;
         _characterController.Move(velocity * Time.deltaTime);
        }

        else 
        {
        _characterController.Move(direction * Time.deltaTime - Vector3.up * 0.1f);
        }
    }

    public void OnGUI() {
        if (currentCameraController != null && currentCameraController.CanSwitch()) {
            GUI.Label(new Rect(0, Screen.height - 40, 300, 20), "PRESS X TO CHANGE CAMERA");
        }
    }

}