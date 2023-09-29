using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OrderedInteractable : Interactable {
    public bool CycleInteraction = false;
    private Queue<Interactable> queue;

    private void Start() {
        CollectInteractables();
    }

    protected override void OnInteract(GameObject source) {
        if (queue.Count == 0 && CycleInteraction) {
            CollectInteractables();
        }

        if (queue.Count > 0) {
            var interactable = queue.Dequeue();
            if (interactable) {
                interactable.Interact(source);
            }
        }
    }

    private void CollectInteractables() {
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

    public int GetOrder() {
        return 100;
    }
}