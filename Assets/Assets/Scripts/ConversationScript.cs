using UnityEngine;
using System.Collections;
using TMPro; 

public class ConversationScript : MonoBehaviour 
{
  //  public int intelligence = 5;
    public TMP_Text qText;
    
    public void Greet(int intelligence)
    {   
        qText = GetComponent<TextMeshProUGUI>();

        switch (intelligence)
        {
        case 5:
            qText.SetText("Слава Україні!");
            break;
        case 4:
            qText.SetText("Слава нації!");
            break;
        case 3:
           // qText.text=="Whadya want?";
            break;
        case 2:
            qText.SetText("Grog SMASH!");
            break;
        case 1:
            qText.SetText("Ulg, glib, Pblblblblb");
            break;
        default:
            qText.SetText("Incorrect intelligence level.");
            break;
        }
    }
}