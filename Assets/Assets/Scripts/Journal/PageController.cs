using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

class PageController {
    
    private String AllText = "";
    private List<String> Pages = new();

    public void AddText(string text) {
        AllText += text;
        Pages = SplitText(AllText, 400);
    }

    public string GetPage(int index) {
        return Pages[index];
    }

    private static List<string> SplitText(string text, int maxLength) {
        string[] words = text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        var parts = new StringBuilder();
        var result = new List<string>();

        foreach (var word in words) {
            if ((parts.Length + word.Length) < maxLength) {
                parts.Append(word);
                parts.Append(' ');
            } else {
                result.Add(parts.ToString());
                parts.Clear();
                parts.Append(word);
                parts.Append(' ');
            }
        }

        if (parts.Length > 0) {
            result.Add(parts.ToString());
        }

        return result;
    }
}
