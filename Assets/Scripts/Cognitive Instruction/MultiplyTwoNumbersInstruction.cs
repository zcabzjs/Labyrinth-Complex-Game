using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplyTwoNumbersInstruction : CognitiveInstruction
{

    int first;
    int second;

    public override string instruction
    {
        get
        {
            return "What is " + first.ToString() + " x " + second.ToString() + "?";
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
                if (n == first * second)
                {
                    result.Add(n.ToString());
                }
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
                int number = Random.Range(0, 401);
                if (!options.Contains(number.ToString()))
                {
                    options.Add(number.ToString());
                    optionGenerated = true;
                }
            }
        }

        // Insert right answer
        int answer = first * second;
        int index = Random.Range(0, numberOfOptions);
        if (!options.Contains(answer.ToString()))
        {
            Debug.Log("Index: " + index.ToString());
            options[index] = answer.ToString();
        }
        return options;
    }

    public MultiplyTwoNumbersInstruction()
    {
        first = Random.Range(1, 20);
        second = Random.Range(1, 20);
    }

}
