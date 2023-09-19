using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonPlayerCharacter : MonoBehaviour
{
    public GameObject dialogBox;
    private float displayTime = 4.0f;
    private float timerDisplay;

    // Start is called before the first frame update
    void Start()
    {
        dialogBox.SetActive(false);
        timerDisplay = -0.5f;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timerDisplay >= 0)
    {
        timerDisplay -= Time.deltaTime;
        if (timerDisplay < 0)
        {
            dialogBox.SetActive(false);
        }
    }
    }

    public void displayDialog()
    {
        ConversationScript id = GetComponent<ConversationScript>();
        timerDisplay = displayTime;
        dialogBox.SetActive(true);
        id.Greet(3);
        //id.intelligence = num;
       
    }
}
