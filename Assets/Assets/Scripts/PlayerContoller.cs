using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerContoller : MonoBehaviour {
    public float speed = 5f;
    public float turningSpeed = 180f;
    public CameraController currentCameraController;

    private float _gravity = -9.81f;
    private Vector3 _velocity;
    [SerializeField] private float jumpHeight;

    private CharacterController _characterController;
    private DialogueController _dialogueController;

    // Start is called before the first frame update
    void Start() {
        _characterController = GetComponent<CharacterController>();
        _dialogueController = FindObjectOfType<DialogueController>();
    }

    // Update is called once per frame
    void Update() {
        if (_dialogueController.HasActiveDialogue()) {
            return;
        }
        tryMove();
        TrySwitchCamera();
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
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
        bool isGrounded = _characterController.isGrounded;
        if (isGrounded && _velocity.y < 0) {
            _velocity.y = 0;
        }

        transform.Rotate(0, Input.GetAxis("Horizontal") * Time.deltaTime * turningSpeed, 0);
        // _direction.x = Input.GetAxis("Vertical");
        // _direction.z = transform.forward.magnitude;

        _characterController.Move(transform.forward * Input.GetAxis("Vertical") * Time.deltaTime * speed);

        if (Input.GetKeyUp(KeyCode.Space) && isGrounded) {
            print("JUMPING");
            _velocity.y += Mathf.Sqrt(jumpHeight * -3.0f * _gravity);
        }

        _velocity.y += _gravity * Time.deltaTime;
        _characterController.Move(_velocity * Time.deltaTime);
    }

    public void OnGUI() {
        if (currentCameraController != null && currentCameraController.CanSwitch()) {
            GUI.Label(new Rect(0, Screen.height - 40, 300, 20), "PRESS X TO CHANGE CAMERA");
        }
    }
}