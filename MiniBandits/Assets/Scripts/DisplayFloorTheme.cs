using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayFloorTheme : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI text;
     

    void Awake()
    {
        StartCoroutine(Display());
    }
    IEnumerator Display()
    {
        yield return new WaitForSeconds(0.1f);
        text.gameObject.SetActive(true);

        string displayText="";
        string rawText = GameManager.currentTheme.ToString();

        for (int i = 0; i < rawText.Length; i++)
        {
            // If the current character is a capital letter and it's not the first character
            if (char.IsUpper(rawText[i]) && i > 0)
            {
                // Add a space before the capital letter
                displayText += " ";
            }

            // Add the current character to the output string
            displayText += rawText[i];
        }

        // Output the resulting string with spaces before every capital letter
        text.text = displayText;

        yield return new WaitForSeconds(0.5f); 
    }
 
}
