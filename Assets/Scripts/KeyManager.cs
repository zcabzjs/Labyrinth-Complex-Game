using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeyManager : MonoBehaviour {

    // Character strings
    const string upperCaseCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    const string lowerCaseCharacters = "abcdefghijklmnopqrstuvwxyz";
    const string numericCharacters = "0123456789";

    public string mixedCharacters = upperCaseCharacters + numericCharacters;

    // Initial number of keys to remember
    public int numberOfKeys = 4;

    // List that stores the number of keys generated
    public List<string> initialKeyArray = new List<string>();

    public TextMeshProUGUI displayKeyText;

    public void GenerateKeys()
    {
        string chosenOption = mixedCharacters;
        for(int i = 0; i < numberOfKeys; i++)
        {
            bool isKeyGenerated = false;
            while (!isKeyGenerated)
            {
                int r = UnityEngine.Random.Range(0, chosenOption.Length - 1);
                string chosenString = chosenOption[r].ToString();
                if (!initialKeyArray.Contains(chosenString))
                {
                    initialKeyArray.Add(chosenString);
                    isKeyGenerated = true;
                }
            }
        }
        UpdateDisplayKeyText();
    }

    void UpdateDisplayKeyText()
    {
        displayKeyText.text = string.Join(" ", initialKeyArray.ToArray());
    }
}
