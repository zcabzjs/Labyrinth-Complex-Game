using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReachAndTouchObstacle : CognitiveObstacle
{

    string completeText = "Swipe left with the right hand to proceed.";
    string instructionText = "Touch the pads with the right answers";

    UIManager uiManager;

    ScoreManager scoreManager;

    SuccessManager successManager;

    DisplayFrame[] frames;

    GameObject arrow;

    [SerializeField]
    List<string> checkedAnswers;

    [SerializeField]
    List<string> choicesToPutOnButton;

    [SerializeField]
    List<string> correctAnswers;

    [SerializeField]
    string instruction;

    Component[] displayFrames;

    Animator anim;
    Animator doorAnim;
    //public bool animationPlaying;
    public float animationTime = 1f;

   

    bool firstPhaseCompleted = false;

    bool wrongAnswerSelected = false;


    public override void InteractWithObstacle(string instruction)
    {
        if (firstPhaseCompleted)
        {
            if (instruction.Equals("SwipeLeft"))
            {
                StartCoroutine(SecondPhaseComplete());
            }
        }
    }

    public override void UpdateInstructionForObstacle()
    {
        //
        uiManager.UpdateInstruction("Step on foot pad to solve puzzle.");
    }

    public override void ActivateObstacle()
    {
        // Make panels appear with question I guess...
        for (int i = 0; i < frames.Length; i++)
        {
            // Set the display frames as disabled
            frames[i].gameObject.SetActive(true);
        }
        // Set question here....
        uiManager.UpdateQuestion(instruction);
        uiManager.UpdateInstruction(instructionText);
    }
    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        scoreManager = GameObject.Find("Score Manager").GetComponent<ScoreManager>();
        successManager = GameObject.Find("Success Manager").GetComponent<SuccessManager>();
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    // Function is run when object is initialised
    public override void Initialise(CognitiveInstruction instruction)
    {
        // This function is run when object is initialised
        GenerateQuestion(instruction);
        GenerateKeyOptions();
        SetAnswerText();

        // Hide the arrows and frames after initialising them
        HideArrowAndFrames();
    }

    void GenerateQuestion(CognitiveInstruction instruction)
    {
        cognitiveInstruction = instruction;
    }

    void HideArrow()
    {
        arrow = this.transform.Find("Arrow").gameObject;
        arrow.SetActive(false);
    }

    void ShowArrow()
    {
        arrow = this.transform.Find("Arrow").gameObject;
        arrow.SetActive(true);
    }

    void HideFrames()
    {
        frames = gameObject.GetComponentsInChildren<DisplayFrame>();
        for (int i = 0; i < frames.Length; i++)
        {
            // Set the display frames as disabled
            frames[i].gameObject.SetActive(false);
        }
    }

    void HideArrowAndFrames()
    {
        HideFrames();
        HideArrow();

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
                ClearFirstPhase();
            }
            successManager.PlaySuccessSound();
            return true;
        }
        if (!correctAnswers.Contains(text))
        {
            wrongAnswerSelected = true;
            successManager.PlayFailureSound();
        }
        return false;
    }

    private void ClearFirstPhase()
    {
        StartCoroutine(FirstPhaseComplete());
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

    IEnumerator FirstPhaseComplete()
    {
        doorAnim = this.transform.Find("Door").GetComponent<Animator>();
        doorAnim.SetTrigger("PushDoor");
        // Might play audio here or do something to show that 1st phase is completed
        yield return new WaitForSeconds(animationTime);
        HideFrames();
        ShowArrow();
        uiManager.UpdateInstruction(completeText);
        uiManager.FadeQuestion();
        firstPhaseCompleted = true;

        if (!wrongAnswerSelected)
        {
            scoreManager.UpdateScore(1);
        }
        else
        {
            scoreManager.UpdateScore(2);
        }
    }

    private void ClearSecondPhase()
    {
        StartCoroutine(SecondPhaseComplete());
    }

    IEnumerator SecondPhaseComplete()
    {
        anim.SetTrigger("PushBoxLeft");
        // Play animation like the push left
        yield return new WaitForSeconds(animationTime);
        isCleared = true;
        uiManager.FadeInstruction();
    }
}
