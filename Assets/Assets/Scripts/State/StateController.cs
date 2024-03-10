using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StateController : MonoBehaviour {
    public readonly Dictionary<string, State> states = new();


    public bool GetBoolState(string id) {
        return states.TryGetValue(id, out var state) ? (bool) state.GetValue() : false;
    }

    public void AddBoolState(DialogueChoice choice) {
        this.AddBoolState(choice.ID, true);
    }
    
    public void AddBoolState(JournalAsset.Choice choice) {
        this.AddBoolState(choice.id, true);
    }

    public void AddBoolState(string id, bool value=true)
    {
        if (states.ContainsKey(id)) return;
        states[id] = new BooleanState(id, value);
    }
}