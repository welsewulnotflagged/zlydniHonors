//using UnityEditor.Animations;

using Assets.Scripts;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour {
    public float speed = 5f;
    public float turningSpeed = 180f;
    public CameraController currentCameraController;
    public bool canMove;
    public AudioClip NotificationSound;
    private GameObject _notificationIndicator;

    public float ymouseSensitivity = 30f;
    public float xmouseSensitivity = 50f;
    private float verticalRotation = 0f;
    public bool lockedCursor = true;
    private const float Gravity = -29.81f; // damn look at that GRAVITY
    private Vector3 _velocity;
    [SerializeField] private float jumpHeight;
    private InventoryController inventory;
    public bool isHidden;
    public bool canRun;


    private CharacterController _characterController;
    private DialogueController _dialogueController;
    private Animator _animator;
    private AudioSource _audioSource;
    public JournalController _journalController;
    private UIController _uiController;


    // Start is called before the first frame update
    void Start()
    {
        canRun = false; 
        _characterController = GetComponent<CharacterController>();
        _dialogueController = FindObjectOfType<DialogueController>();
        _audioSource = GetComponent<AudioSource>();
        _animator = GetComponentInChildren<Animator>();
        _uiController = FindObjectOfType<UIController>();
        // _journalController.OpenJournalMenu();
        //  _journalController = GetComponentInChildren<JournalController>();
        _notificationIndicator = GameObject.Find("IndicatorNewEntry");
        ToggleNotification(false);
    }

    // Update is called once per frame
    void Update() {
        Cursor.lockState = lockedCursor ? CursorLockMode.Locked : CursorLockMode.None;

        if (_dialogueController.HasActiveDialogue() || _uiController.HasActiveChoices()) {
            return;
        }

        if (!canMove) {
            tryMove();
            TrySwitchCamera();
        }

        //tryMove();
        //TrySwitchCamera();
        if (Input.GetKey(KeyCode.Escape)) {
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.Q)) {
            if (_journalController != null) {
                _journalController.OpenJournalMenu();
                _journalController.isOpen = !_journalController.isOpen;
                lockedCursor = !_journalController.isOpen;
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

    public void ToggleNotification(bool show) {
        _notificationIndicator.SetActive(show);
        if (show) {
            _audioSource.PlayOneShot(NotificationSound);
        }
    }

    private void tryMove() {
        bool isGrounded = _characterController.isGrounded;
        if (isGrounded && _velocity.y < 0) {
            _velocity.y = 0;
        }

        _animator.SetFloat("h", Input.GetAxis("Vertical"));

        var localSpeed = speed;
        if (canRun)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                _animator.SetBool("sprint", true);
                localSpeed *= 2;
            }
            else
            {
                _animator.SetBool("sprint", false);
            }
        }
        
        if (lockedCursor) {
            float mouseX = Input.GetAxis("Mouse X") * xmouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * ymouseSensitivity * Time.deltaTime;

            transform.Rotate(Vector3.up, mouseX);

            verticalRotation -= mouseY;
            verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);
            transform.localRotation = Quaternion.Euler(verticalRotation, transform.localEulerAngles.y, 0f);
        }

        _characterController.Move(transform.forward * Input.GetAxis("Vertical") * Time.deltaTime * localSpeed);
        _characterController.Move(transform.right * Input.GetAxis("Horizontal") * Time.deltaTime * localSpeed);
        

        if (Input.GetKeyUp(KeyCode.Space) && isGrounded) {
            print("JUMPING");
            _velocity.y += Mathf.Sqrt(jumpHeight * -3.0f * Gravity);
        }

        _velocity.y += Gravity * Time.deltaTime;
        _characterController.Move(_velocity * Time.deltaTime);
    }
}