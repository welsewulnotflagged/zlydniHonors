using UnityEngine;

public abstract class Interactable : MonoBehaviour {
    public int Order = 0;
    
    public abstract void Interact(GameObject source);

    public int GetOrder() {
        return Order;
    }
}