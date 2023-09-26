using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "My assets/Item", order = 100)]
public class InventoryAsset : ScriptableObject {
    [SerializeField]
    public List<ItemAsset> Pickups = new();
}
