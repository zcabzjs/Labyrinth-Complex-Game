using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreeChooseOneObstacle : Obstacle
{
    [SerializeField]
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

    UIManager uiManager;

    Animator anim;
    public bool animationPlaying;
    public float animationTime = 1f;

    public override void InteractWithObstacle(string instruction)
    {
        if (instruction.Equals("SwipeLeft"))
        {
            // Do something
            DisplayFrame displayFrame = displayFrames[0] as DisplayFrame;
            displayFrame.ChooseFrame();
        }
        else if (instruction.Equals("SwipeRight"))
        {
            // Do something
            DisplayFrame displayFrame = displayFrames[2] as DisplayFrame;
            displayFrame.ChooseFrame();
        }
        else if (instruction.Equals("SwipeUp")){
            // Do something
            DisplayFrame displayFrame = displayFrames[1] as DisplayFrame;
            displayFrame.ChooseFrame();
        }
        else if (instruction.Equals("PushDoor"))
        {
            // Check if player allowed to go through (puzzle is completed)
        }
        
    }

    public override void UpdateInstructionForObstacle()
    {
        uiManager.UpdateInstruction(instruction);
    }

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        displayFrames = GetComponentsInChildren<DisplayFrame>();
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
        
        for (int i = 0; i < displayFrames.Length; i++)
        {
            DisplayFrame displayFrame = displayFrames[i] as DisplayFrame;
            displayFrame.SetDisplayText(choicesToPutOnButton[i]);
        }
    }

    // Check if answer put is in the desired answers, and add them into the list if it is correct
    public bool CheckAnswer(string text)
    {
        if (correctAnswers.Contains(text) && !checkedAnswers.Contains(text))
        {
            checkedAnswers.Add(text);
            if (IsAnswersComplete())
            {
                ClearObstacle();
            }
            return true;
        }
        return false;
    }

    IEnumerator PlayAnimation()
    {
        anim.SetTrigger("PushDoor");
        yield return new WaitForSeconds(animationTime);
        isCleared = true;
        uiManager.FadeInstruction();
    }

    private void ClearObstacle()
    {
        Debug.Log("Puzzle complete!");
        StartCoroutine(PlayAnimation());
    }

    // Check if answer by player is complete
    bool IsAnswersComplete()
    {
        // Allow door to open if so..
        if (checkedAnswers.Count == correctAnswers.Count)
        {
            return true;
        }
        return false;
    }
}
