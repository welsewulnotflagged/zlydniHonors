
//using UnityEditor.Animations;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour {
    public float speed = 5f;
    public float turningSpeed = 180f;
    public CameraController currentCameraController;
    public bool canMove;

    private float _gravity = -9.81f;
    private Vector3 _velocity;
    [SerializeField] private float jumpHeight;
    private InventoryController inventory;


    private CharacterController _characterController;
    private DialogueController _dialogueController;
    private Animator _animator;
    public JournalController _journalController;

    // Start is called before the first frame update
    void Start() {
        _characterController = GetComponent<CharacterController>();
        _dialogueController = FindObjectOfType<DialogueController>();
        _animator = GetComponentInChildren<Animator>();
         // _journalController.OpenJournalMenu();
      //  _journalController = GetComponentInChildren<JournalController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_dialogueController.HasActiveDialogue())
        {
            return;
        }

        if (!canMove)
        {
            tryMove();
            TrySwitchCamera();
        }

        //tryMove();
        //TrySwitchCamera();
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
        
        if (Input.GetKeyDown(KeyCode.Q))
        {    if (_journalController != null)
            {
                _journalController.OpenJournalMenu();
                _journalController.isOpen = !_journalController.isOpen;
            }
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

        _animator.SetFloat("h", Input.GetAxis("Vertical"));
        
        transform.Rotate(0, Input.GetAxis("Horizontal") * Time.deltaTime * turningSpeed, 0);
        // _direction.x = Input.GetAxis("Vertical");
        // _direction.z = transform.forward.magnitude;

        var localSpeed = speed;
        if (Input.GetKey(KeyCode.LeftShift)) {
            _animator.SetBool("sprint", true);
            localSpeed *= 2;
        } else {
            _animator.SetBool("sprint", false);
        }
        _characterController.Move(transform.forward * Input.GetAxis("Vertical") * Time.deltaTime * localSpeed);

        if (Input.GetKeyUp(KeyCode.Space) && isGrounded) {
            print("JUMPING");
            _velocity.y += Mathf.Sqrt(jumpHeight * -3.0f * _gravity);
        }

        _velocity.y += _gravity * Time.deltaTime;
        _characterController.Move(_velocity * Time.deltaTime);
    }


}

