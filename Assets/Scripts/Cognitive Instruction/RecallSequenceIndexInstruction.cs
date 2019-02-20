using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecallSequenceIndexInstruction : CognitiveInstruction
{
    const string upperCaseCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    const string numericCharacters = "0123456789";
    string mixedCharacters = upperCaseCharacters + numericCharacters;

    int recallIndex = 0;

    public RecallSequenceIndexInstruction(int index)
    {
        recallIndex = index;
    }

    private string GetNumberSuffix()
    {
        // Only for < 10
        switch (recallIndex)
        {
            case 1:
                return "st";
            case 2:
                return "nd";
            case 3:
                return "rd";
            default:
                return "th";
        }
    }

    public override string instruction
    {
        get
        {
            return "What is the " + recallIndex + GetNumberSuffix() + " character in the given initial sequence?";
        }
    }

    public override List<string> ProduceRightAnswers(List<string> input)
    {
        KeyManager keyManager = GameObject.Find("KeyManager").GetComponent<KeyManager>();
        List<string> correctAnswers = keyManager.initialKeyArray;
        List<string> result = new List<string>();
        result.Add(correctAnswers[recallIndex - 1]);
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
        KeyManager keyManager = GameObject.Find("KeyManager").GetComponent<KeyManager>();
        List<string> correctAnswers = keyManager.initialKeyArray;
        if (!options.Contains(correctAnswers[recallIndex - 1]))
        {
            options[index] = correctAnswers[recallIndex - 1];
        }
        return options;
    }
}
