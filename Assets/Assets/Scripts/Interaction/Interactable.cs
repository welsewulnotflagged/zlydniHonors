using UnityEngine;

public abstract class Interactable : MonoBehaviour {
    public int Order = 0;
    public string Prompt = "INTERACT";

    protected bool CanInteract(GameObject source) {
        return true;
    }

    protected abstract void OnInteract(GameObject source);

    public string GetPrompt() {
        return Prompt;
    }

    public void Interact(GameObject source) {
        if (CanInteract(source)) {
            OnInteract(source);
        }
    }

    public int GetOrder() {
        return Order;
    }
}