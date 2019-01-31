using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreeChooseOneObstacle : Obstacle
{
    List<string> checkedAnswers;

    [SerializeField]
    List<string> choicesToPutOnButton;

    // Instruction for the obstacle
    CognitiveInstruction cognitiveInstruction;

    Component[] displayFrames;

    [SerializeField]
    List<string> correctAnswers;

    [SerializeField]
    string instruction;

    public override void InteractWithObstacle(string instruction)
    {
        if (instruction.Equals("SwipeLeft"))
        {
            // Do something
        }
        else if (instruction.Equals("SwipeRight"))
        {
            // Do something
        }
        else if (instruction.Equals("SwipeUp")){
            // Do something
        }
        else if (instruction.Equals("PushDoor"))
        {
            // Check if player allowed to go through (puzzle is completed)
        }
        
    }

    public override void UpdateInstructionForObstacle()
    {
        throw new System.NotImplementedException();
    }

    // Use this for initialization
    void Start () {
        Initialise();
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void Initialise()
    {
        // This function is run when object is initialised
        GenerateQuestion();
        GenerateKeyOptions();
        SetAnswerText();
    }

    void GenerateQuestion()
    {
        cognitiveInstruction = new AddTwoNumbersInstruction();


    }

    void GenerateKeyOptions()
    {
        checkedAnswers = new List<string>();
        choicesToPutOnButton = cognitiveInstruction.ProduceOptions();

        // For testing purposes
        correctAnswers = cognitiveInstruction.ProduceRightAnswers(choicesToPutOnButton);
        instruction = cognitiveInstruction.instruction;
    }

    void SetAnswerText()
    {
        displayFrames = GetComponentsInChildren<DisplayFrame>();
        for (int i = 0; i < displayFrames.Length; i++)
        {
            DisplayFrame displayFrame = displayFrames[i] as DisplayFrame;
            displayFrame.SetDisplayText(choicesToPutOnButton[i]);
        }
    }
}
