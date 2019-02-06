using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumbersOnlyInstruction : CognitiveInstruction
{
    const string upperCaseCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    const string numericCharacters = "0123456789";
    string mixedCharacters = upperCaseCharacters + numericCharacters;

    public override string instruction
    {
        get
        {
            return "Interact with numbers only";
        }
    }

    public override List<string> ProduceRightAnswers(List<string> input)
    {
        List<string> result = new List<string>();
        foreach (string s in input)
        {
            int n;
            if (int.TryParse(s, out n))
            {
                result.Add(s);
            }
        }
        return result;
    }

    public override List<string> ProduceOptions()
    {
        List<string> options = new List<string>();
        for (int i = 0; i < numberOfOptions; i++)
        {
            bool optionGenerated = false;
            while (!optionGenerated)
            {
                int number = Random.Range(0, mixedCharacters.Length);
                if (!options.Contains(mixedCharacters[number].ToString()))
                {
                    options.Add(mixedCharacters[number].ToString());
                    optionGenerated = true;
                }
            }
        }

        // Insert right answer
        int index = Random.Range(0, numberOfOptions);
        int randomIndex = Random.Range(0, numericCharacters.Length);
        if (!options.Contains(numericCharacters[randomIndex].ToString()))
        {
            options[index] = numericCharacters[randomIndex].ToString();
        }
        return options;
    }
}
