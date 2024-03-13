using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freezer : MonoBehaviour
{
    public static Freezer Instance { get; private set; }
    public bool isFrozen;

    private float original;

    // Update is called once per frame
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // DontDestroyOnLoad(gameObject);
            original = Time.timeScale;
            Debug.Log("OG time set" + original);
        }
    }


    public void Unfreeze()
    {
            isFrozen =  false;
            Time.timeScale = 1f;
            Debug.Log("Unfreeze called + timeScale:" +original );
    }

//until Un-Freezed
    public void DoFreeze()
    {
        isFrozen = true;
        Time.timeScale = 0f;
        Debug.Log("Frozen :" + isFrozen);
    }
}