using UnityEngine;

[ExecuteInEditMode]
public class Pickable : Interactable {
    public ItemAsset item;
    public float radius = 3f;
    private Mesh mesh;

    private GameObject _spawnedObj;
    private InventoryController _inventoryController;

    // Start is called before the first frame update
    void Start() {
        _inventoryController = FindObjectOfType<InventoryController>();
        
        if (Application.isPlaying) {
            var collider = gameObject.AddComponent<SphereCollider>();
            collider.radius = radius;
            collider.isTrigger = true;
            _spawnedObj = Instantiate(item.obj, transform);
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);
        item.obj.GetComponent<MeshRenderer>().sharedMaterial.SetPass(0);
        Graphics.DrawMeshNow(item.obj.GetComponent<MeshFilter>().sharedMesh, transform.position, Quaternion.identity, 0);
    }

    protected override void OnInteract(GameObject source) {
        Destroy(_spawnedObj);
        Debug.Log("PICK UP ITEM" + item.title);
        _inventoryController.Add(item, 1);
        gameObject.SetActive(false);
    }
}