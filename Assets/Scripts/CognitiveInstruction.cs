using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CognitiveInstruction : MonoBehaviour {

    public int numberOfOptions = 3;
    // Function to set and get instruction
    public virtual string instruction
    {
        get
        {
            return "Default instruction for cognitive question";
        }
    }

    // Function to produce the right answer
    public virtual List<string> ProduceRightAnswers(List<string> input)
    {
        return input;
    }

    public virtual List<string> ProduceOptions()
    {
        return new List<string>();
    }
}
