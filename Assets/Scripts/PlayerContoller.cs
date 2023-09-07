
using UnityEngine;

public class PlayerContoller : MonoBehaviour {
    public float speed = 5f;
    private CharacterController _characterController;
    private GameObject _hitObject;
    public float jumpHeight = 15f; 
    private float y_velocity; 

    // Start is called before the first frame update
    void Start() {
        _characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update() {

        tryMove();
        tryRaycast();
        tryInteract();
    }

    public void OnGUI() {
        if (_hitObject != null) {
            GUI.Label(new Rect(Screen.width/2, Screen.height/2, 50, 20), _hitObject.name);
            GUI.Label(new Rect(Screen.width/2, Screen.height/2+20, 80, 20), "PRESS E");
        }
    }

    private void tryRaycast() {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 10f) && hit.collider.gameObject.GetComponent<Interactable>() != null) {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.green);
            _hitObject = hit.collider.gameObject;
        } else {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 100, Color.red);
            _hitObject = null;
        }
    }

    private void tryMove() {
        Vector3 direction;
        

        transform.Rotate(0, Input.GetAxis("Horizontal") * Time.deltaTime * 180f, 0);
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

    private void tryInteract() {
        if (Input.GetKeyDown(KeyCode.E) && _hitObject != null) {
            Debug.Log("KEYCODe");
            _hitObject.GetComponent<Interactable>().Interact(gameObject);
        }
    }

}