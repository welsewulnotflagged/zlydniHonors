using System;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class DialogueChoice {
    public string ID;
    public string ChoiceText;
    [AllowsNull]
    public string NextDialogueID;
    [AllowsNull] 
    public string AffectState;
    [AllowsNull] 
    public string TriggerState;
}