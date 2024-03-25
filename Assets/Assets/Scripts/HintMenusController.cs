using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintMenusController : MonoBehaviour
{
    //   public GameObject hintMenuCanvas;

    public GameObject hint;
    private Freezer _freezer;
    public bool triggersEnemies;
    public GameObject[] enemies;
    private PlayerController _playerController;

    // public BoxCollider[] triggerBoxes;
    // Start is called before the first frame update
    void Start()
    {
        _playerController = FindObjectOfType<PlayerController>();
        _freezer = Freezer.Instance;
        hint.SetActive(false);
        foreach (var enemy in enemies)
        {
            enemy.SetActive(false);
        }
    }

    // Update is called once per frame

    private void OnTriggerEnter(Collider other)
    {
        hint.SetActive(true);
        var collider = hint.GetComponentInParent<BoxCollider>();
        collider.enabled = false;
        if (_freezer != null) {
            _playerController.lockedCursor = false;
            _freezer.DoFreeze();
            Debug.Log("Freeze called");
            foreach (var enemy in enemies)
            {
                enemy.SetActive(true);
                _playerController.canRun = true;
            }
        }
        
        else if (_freezer == null)
        {
            Debug.Log("no freezer");
            if (triggersEnemies)
            {
                
            }
        }
    }
}