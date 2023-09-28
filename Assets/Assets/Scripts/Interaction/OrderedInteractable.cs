using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OrderedInteractable : Interactable {
    private Queue<Interactable> queue;

    private void Start() {
        queue = new Queue<Interactable>();
        List<Interactable> interactables = new List<Interactable>(GetComponents<Interactable>());
        interactables.Sort((a, b) => a.GetOrder().CompareTo(b.GetOrder()));
        interactables.ForEach((a) => {
            if (a != this) {
                queue.Enqueue(a);
            }
        });
        Debug.Log(queue.ToList());
    }

    public override void Interact(GameObject source) {
        var interactable = queue.Dequeue();
        if (interactable) {
            interactable.Interact(source);
        }
    }

    public int GetOrder() {
        return 100;
    }
}