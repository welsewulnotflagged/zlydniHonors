using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContoller : MonoBehaviour
{
    public float speed = 5f;
    private CharacterController _characterController;

    // Start is called before the first frame update
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction;
        
        transform.Rotate(0, Input.GetAxis("Horizontal") * Time.deltaTime * 180f, 0);
        direction = transform.forward * Input.GetAxis("Vertical") * speed;

        _characterController.Move(direction * Time.deltaTime - Vector3.up * 0.1f);
    }
}