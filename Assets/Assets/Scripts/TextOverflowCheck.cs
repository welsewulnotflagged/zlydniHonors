using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextOverflowCheck : MonoBehaviour {
    public Text leftPageTextArea;
    public Text rightPageTextArea;
    public static int maxCharacterCount = 418;
    public string overflowText = "";


    public TextOverflowCheck(Text leftPageTextArea, Text rightPageTextArea) {
        this.leftPageTextArea = leftPageTextArea;
        this.rightPageTextArea = rightPageTextArea;
    }

    public void CheckAndHandleOverflow(string ogText) {
        maxCharacterCount = 418;
        if (TextOverflow(ogText, maxCharacterCount)) {
            int excessLength = ogText.Length - maxCharacterCount;

            if (excessLength <= 0) {
                Debug.Log($"{ogText.Length}");
                return;
            }

            int endIndex = Mathf.Max(0, ogText.Length - excessLength);
            Debug.Log($" length: {excessLength}, endIndex: {endIndex}");
            overflowText = ogText.Substring(endIndex, excessLength);
            leftPageTextArea.text = ogText.Substring(0, endIndex);
        } else {
            // No overflow, update the left page with the full text
            leftPageTextArea.text = ogText;
            overflowText = ""; // Clear overflow text
        }

        rightPageTextArea.text += overflowText;
    }


    private bool TextOverflow(string text, int maxCharacters) {
        return text.Length > maxCharacters;
    }
}