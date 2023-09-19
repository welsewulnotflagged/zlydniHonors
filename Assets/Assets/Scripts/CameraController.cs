using System;
using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private CinemachineVirtualCamera _camera;

    public void Start()
    {
        _camera = GetComponentInChildren<CinemachineVirtualCamera>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _camera.Priority = 2;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _camera.Priority = 0;
        }
    }
}