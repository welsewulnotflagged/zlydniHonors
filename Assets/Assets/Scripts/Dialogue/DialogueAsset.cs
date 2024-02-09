using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "My assets/Dialogue", order = 100)]
public class DialogueAsset : ScriptableObject {
    //public int id;
    //[TextArea] public string[] dialogue;
    
      //  using UnityEngine;
    
  //  [System.Serializable]
  
        public int id;
        [TextArea] public string[] dialogue;
        public List<Choice> choices;
    

    [System.Serializable]
    public class Choice
    {
        public string choiceText;
        public int nextDialogueID;
        public static Choice exitChoice = new Choice { choiceText = "Exit", nextDialogueID = -1 };
    }

    /*public DialogueAsset[] dialogueNodes;

    private void OnEnable()
    {
        // Ensure exit choice is added to each dialogue node on asset creation or re-creation
        foreach (var node in dialogueNodes)
        {
            // If exit choice is not set, set it
            if (node.exitChoice == null)
            {
                node.exitChoice = exitChoice;
            }
        }
    }*/
}
        