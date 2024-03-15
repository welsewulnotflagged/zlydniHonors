using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StateController : MonoBehaviour {
    public readonly Dictionary<string, State> states = new();
    private PlayerController _playerController;


    private void Start() {
        _playerController = FindObjectOfType<PlayerController>();
    }

    public bool GetBoolState(string id) {
        return states.TryGetValue(id, out var state) ? (bool) state.GetValue() : false;
    }

    public void AddBoolState(DialogueChoice choice, bool showNotification=true) {
        this.AddBoolState(choice.ID, true);
        if (showNotification) {
            _playerController.ToggleNotification(true);
        }
    }
    
    public void AddBoolState(JournalAsset.Choice choice, bool showNotification=true) {
        this.AddBoolState(choice.id, true);
        if (showNotification) {
            _playerController.ToggleNotification(true);
        }
    }

    public void AddBoolState(string id, bool value=true)
    {
        if (states.ContainsKey(id)) return;
        states[id] = new BooleanState(id, value);
    }
    
}