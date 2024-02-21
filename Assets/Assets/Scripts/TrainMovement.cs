using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainMovement : MonoBehaviour
{
    public float speed = 10f;

    private bool isMoving = false;

    void Update()
    {
        if (isMoving)
        {
            // Move the train forward
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }

    public void StartMoving()
    {
        isMoving = true;
    }

    public void StopMoving()
    {
        isMoving = false;
    }
}