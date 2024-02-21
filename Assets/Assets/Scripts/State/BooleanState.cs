using System;

[Serializable]
public class BooleanState : State {
    private readonly string _id;
    private readonly bool _value;

    public BooleanState(string id, bool value) {
        _id = id;
        _value = value;
    }
    
    public string GetId() {
        return _id;
    }

    public object GetValue() {
        return _value;
    }
}