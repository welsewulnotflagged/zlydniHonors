using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEnemiesPan : MonoBehaviour
{
    private GameObject[] enemies;

    private CameraController _cameraController;

    // Start is called before the first frame update
    void Start()
    {
        if (enemies == null)
        {
            enemies = GameObject.FindGameObjectsWithTag("Enemy");

            foreach (GameObject enemy in enemies)
            {
                var _cameraController = FindObjectOfType<CameraController>();
                Debug.Log("camera found");
            }
        }
    }

    public void OnButtonPan()
    {
        foreach (var enemy in enemies)
        {
            _cameraController.SwitchCamera();
        }
    }

    // Update is called once per frame
}