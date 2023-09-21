using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class Pickable : MonoBehaviour {
    public ItemAsset item;
    public float radius = 3f;
    public Mesh mesh;

    private GameObject _spawnedObj;

    // Start is called before the first frame update
    void Start() {
        if (Application.isPlaying) {
            var collider = gameObject.AddComponent<SphereCollider>();
            collider.radius = radius;
            collider.isTrigger = true;
            // PrefabUtility.InstantiatePrefab()
            _spawnedObj = Instantiate(item.obj, transform);
        }
    }


// Update is called once per frame
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            Destroy(_spawnedObj);
            Debug.Log("PICK UP ITEM" + item.title);
            gameObject.SetActive(false);
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);
        Debug.Log(item.obj.GetComponent<MeshFilter>());
        item.obj.GetComponent<MeshRenderer>().sharedMaterial.SetPass(0);
        Graphics.DrawMeshNow(item.obj.GetComponent<MeshFilter>().sharedMesh, transform.position, Quaternion.identity, 0);
    }

}