using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionGenerator : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public CognitiveInstruction GenerateQuestion()
    {
        // Max number is based on number of instructions in total created...
        // Look at the number of switch choices to set max number for this
        int numberOfQuestions = 3;
        int choice = UnityEngine.Random.Range(0, numberOfQuestions);
        // Add questions here
        switch (choice)
        {
            case 0:
                return new AddTwoNumbersInstruction();
         
            case 1:
                return new NumbersOnlyInstruction();

            case 2:
                return new SubtractTwoNumbersInstruction();

            default:
                Debug.Log("Error in generating question, check QuestionGenerator.cs");
                return null;
        }
    }
}
