using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextOverflowCheck : MonoBehaviour {
   // public Text leftPageTextArea;
   
    private int textAreaIndex;
    public static int maxCharacterCount = 418;
    public string overflowText = "";
    public int currentPage = 0;
    public Button buttonPrefab; 
    public Transform buttonContainer;

    private void Start()
    {
        if (buttonContainer == null)
        {
            Debug.LogError("Button Container not assigned in the inspector!");
            return;
        }
        CreateNextButton();
        CreatePreviousButton();
        textAreaIndex = 0;

    }

    // public TextOverflowCheck(Text leftPageTextArea, Text rightPageTextArea) {
    //     this.leftPageTextArea = leftPageTextArea;
    //     this.rightPageTextArea = rightPageTextArea;
    // }

   /* public void CheckAndHandleOverflow(string ogText) {
        maxCharacterCount = 418;
        if (TextOverflow(ogText, maxCharacterCount)) {
            int excessLength = ogText.Length - maxCharacterCount;

            if (excessLength <= 0) {
                Debug.Log($"{ogText.Length}");
                return;
            }

            int endIndex = Mathf.Max(0, ogText.Length - excessLength);
            overflowText = ogText.Substring(endIndex, excessLength);
            leftPageTextArea.text = ogText.Substring(0, endIndex);
        } else {
            leftPageTextArea.text = ogText;
            overflowText = ""; // Clear overflow text
        }

        rightPageTextArea.text += overflowText;
        
        
    }*/
   
   public void CheckAndHandleOverflow(List<string> linesText, Text textArea) {
        
       for (int i = 0; i < linesText.Count; i++)
       {
           if (textArea.text.Length + linesText[i].Length > 418)
           {
               textArea.text = " ";
           }
           textArea.text += linesText[i];
       }
   }


    private bool TextOverflow(string text, int maxCharacters) {
        return text.Length > maxCharacters;
    }
    
    public void ChangePage(int pageChange)
    {
        currentPage += pageChange;
        currentPage = Mathf.Clamp(currentPage, 0, int.MaxValue);
        Debug.Log("Placeholder to check button creation");
    }

   /* public void UpdatePages()
    {
        int leftPageCharacterCount = Mathf.Min(maxCharacterCount, currentPage * maxCharacterCount);
        int rightPageCharacterCount = Mathf.Max(0, (currentPage + 1) * maxCharacterCount - maxCharacterCount);

        // Update left page text
        leftPageTextArea.text = "Left Page:\n";
        
        CheckAndHandleOverflow();
    }*/
   

    private void CreateNextButton() {
        Button turnPageButton = Instantiate(buttonPrefab, buttonContainer);
        turnPageButton.gameObject.SetActive(true);
        
        turnPageButton.GetComponent<Text>().text = "Next";

        turnPageButton.onClick.AddListener(() => ChangePage(1));
        }
    
    private void CreatePreviousButton() {
        Button turnPageButton = Instantiate(buttonPrefab, buttonContainer);
        turnPageButton.gameObject.SetActive(true);
        
        turnPageButton.GetComponent<Text>().text = "Previous";

        turnPageButton.onClick.AddListener(() => ChangePage(-1));
    }
}