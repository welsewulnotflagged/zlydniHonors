using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NonPlayerCharacter : MonoBehaviour {
    public string Name;
    public float Reputation;
    public GameObject LabelPrefab;

    private PlayerController _player;
    private GameObject _uiObject;
    private Slider _slider;

    // Start is called before the first frame update
    void Start() {
        _player = FindObjectOfType<PlayerController>();
        _uiObject = Instantiate(LabelPrefab, transform);
        _slider = GetComponentInChildren<Slider>();


        _uiObject.transform.Translate(Vector3.up);
        GetComponentInChildren<TextMeshPro>().text = Name;
    }

    void Update() {
        _uiObject.transform.LookAt(_uiObject.transform.position + _player.transform.rotation * Vector3.back, _player.transform.rotation * Vector3.up);
        _uiObject.transform.Rotate(0, 180, 0);
        _slider.value = Reputation;
    }
}