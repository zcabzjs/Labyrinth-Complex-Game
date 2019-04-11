using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreeChooseOneObstacle : CognitiveObstacle
{

    public string instructionText = "Swipe your hand in the direction of the arrow of the right answers.";

    [SerializeField]
    List<string> checkedAnswers;

    [SerializeField]
    List<string> choicesToPutOnButton;

    Component[] displayFrames;

    [SerializeField]
    List<string> correctAnswers;

    [SerializeField]
    string instruction;

    UIManager uiManager;

    ScoreManager scoreManager;

    SuccessAudioManager successAudioManager;

    Animator anim;
    public bool animationPlaying;
    public float animationTime = 1f;

    bool wrongAnswerSelected = false;


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
            Debug.Log("Up has been chosen");
            DisplayFrame displayFrame = displayFrames[1] as DisplayFrame;
            displayFrame.ChooseFrame();
        }
        
    }

    public override void UpdateInstructionForObstacle()
    {
        uiManager.UpdateQuestion(instruction);
        uiManager.UpdateInstruction(instructionText);
    }

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        scoreManager = GameObject.Find("Score Manager").GetComponent<ScoreManager>();
        successAudioManager = GameObject.Find("Success Audio Manager").GetComponent<SuccessAudioManager>();
        //Initialise();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void Initialise(CognitiveInstruction instruction)
    {
        // This function is run when object is initialised
        GenerateQuestion(instruction);
        GenerateKeyOptions();
        SetAnswerText();
    }

    void GenerateQuestion(CognitiveInstruction instruction)
    {
        cognitiveInstruction = instruction;


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

    // Check if answer put is in the desired answers, and add them into the list if it is correct
    public override bool CheckAnswer(string text)
    {
        if (correctAnswers.Contains(text) && !checkedAnswers.Contains(text))
        {
            checkedAnswers.Add(text);
            if (IsAnswersComplete())
            {
                ClearObstacle();
            }
            successAudioManager.PlaySuccessSound();
            return true;
        }
        if (!correctAnswers.Contains(text))
        {
            successAudioManager.PlayFailureSound();
            wrongAnswerSelected = true;
        }
        return false;
    }

    IEnumerator PlayAnimation()
    {
        anim.SetTrigger("PushDoor");
        yield return new WaitForSeconds(animationTime);
        isCleared = true;
        uiManager.FadeInstruction();
        uiManager.FadeQuestion();

        if (!wrongAnswerSelected)
        {
            scoreManager.UpdateScore(1);
        }
        else
        {
            scoreManager.UpdateScore(2);
        }
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
