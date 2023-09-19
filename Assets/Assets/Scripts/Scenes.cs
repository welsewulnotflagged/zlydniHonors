using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenes : MonoBehaviour {
    void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Player") {
            Debug.Log("collided");
            SceneManager.LoadScene(1); // check build settings for indexes
            //SceneManager.LoadScene(2);
            //SceneManager.LoadScene(3);
        }
    }
}