using UnityEngine;

[ExecuteInEditMode]
public class Pickable : Interactable {
    public ItemAsset item;
    public float radius = 3f;
    private Mesh mesh;

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

    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);
        Debug.Log(item.obj.GetComponent<MeshFilter>());
        item.obj.GetComponent<MeshRenderer>().sharedMaterial.SetPass(0);
        Graphics.DrawMeshNow(item.obj.GetComponent<MeshFilter>().sharedMesh, transform.position, Quaternion.identity, 0);
    }

    public void Interact(GameObject source) {
        Destroy(_spawnedObj);
        Debug.Log("PICK UP ITEM" + item.title);
        gameObject.SetActive(false);
    }
}