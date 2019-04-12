using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalDoorObstacle : Obstacle {

    string obstacleInstruction = "Touch the characters to complete the initial given sequence.";
    string completeText = "Push the door to proceed";
    string obstacleQuestion = "Complete the whole sequence given initially.";
    string startingInstruction = "Step on the floor tile to start the puzzle.";

    int numberOfButtonsOnDoor = 7; // Number of buttons on door

    [SerializeField]
    bool doorUnlocked;

    List<string> checkedAnswers;

    [SerializeField]
    List<string> initialKeys;

    [SerializeField]
    List<string> choicesToPutOnButton;

    [SerializeField]
    string stringsToChooseFrom;

    [SerializeField]
    List<string> correctAnswers;

    Animator anim;
    public bool animationPlaying;
    public float animationTime = 1f;

    KeyManager keyManager;

    Component[] doorButtons;

    UIManager uiManager;

    ScoreManager scoreManager;

    SuccessAudioManager successAudioManager;

    bool wrongAnswerSelected = false;

    bool sequenceCompleted = false;

    int currentIndex = 0;

    bool correctAnswerSelected = false;

    string finalDisplayedKeys = "";

    bool obstacleActivated = false;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        scoreManager = GameObject.Find("Score Manager").GetComponent<ScoreManager>();
        successAudioManager = GameObject.Find("Success Audio Manager").GetComponent<SuccessAudioManager>();
        // Call initialise here
        Initialise(currentIndex);
        GenerateFinalDisplayedKeyString(currentIndex);
    }

    // Update is called once per frame
    void Update()
    {
    }

    // Initialise the buttons on the door by randomly generating random stuff
    void Initialise(int currentIndex)
    {
        GenerateKeyOptions(currentIndex);
        ShuffleChoices();
        SetButtonText();
        GenerateCorrectAnswers(currentIndex);
    }

    void GenerateFinalDisplayedKeyString(int currentIndex)
    {
        finalDisplayedKeys = "";
        for(int i = 0; i < initialKeys.Count; i++)
        {
            if(i < currentIndex)
            {
                finalDisplayedKeys += initialKeys[i];
            }
            else
            {
                finalDisplayedKeys += " -";
            }
        }
        uiManager.UpdateFinalObstacleKey(finalDisplayedKeys);
    }

    // Generate the options to be put on the door
    void GenerateKeyOptions(int currentIndex)
    {
        checkedAnswers = new List<string>();
        choicesToPutOnButton = new List<string>();

        keyManager = GameObject.Find("KeyManager").GetComponent<KeyManager>();
        initialKeys = keyManager.initialKeyArray;
        stringsToChooseFrom = keyManager.mixedCharacters;
        for (int i = 0; i < numberOfButtonsOnDoor; i++)
        {
            if (i == 0)
            {
                string chosenString = initialKeys[currentIndex].ToString();
                choicesToPutOnButton.Add(chosenString);
            }
            else
            {
                bool isKeyGenerated = false;
                while (!isKeyGenerated)
                {
                    int r = UnityEngine.Random.Range(0, stringsToChooseFrom.Length);
                    string chosenString = stringsToChooseFrom[r].ToString();
                    if (!choicesToPutOnButton.Contains(chosenString))
                    {
                        choicesToPutOnButton.Add(chosenString);
                        isKeyGenerated = true;
                    }
                }
            }
        }
    }

    // Put the generated options onto the buttons
    void SetButtonText()
    {
        doorButtons = GetComponentsInChildren<FinalDoorButton>();
        for (int i = 0; i < doorButtons.Length; i++)
        {
            FinalDoorButton doorButton = doorButtons[i] as FinalDoorButton;
            doorButton.SetButtonText(choicesToPutOnButton[i]);
        }
    }

    // Simple shuffles the list of options
    void ShuffleChoices()
    {
        for (int i = 0; i < choicesToPutOnButton.Count; i++)
        {
            string temp = choicesToPutOnButton[i];
            int randomIndex = Random.Range(i, choicesToPutOnButton.Count);
            choicesToPutOnButton[i] = choicesToPutOnButton[randomIndex];
            choicesToPutOnButton[randomIndex] = temp;
        }
    }

    // Pops the buttons out to allow player to complete the puzzle
    public override void ActivateObstacle()
    {
        PopButtons();
        uiManager.UpdateQuestion(obstacleQuestion);
        uiManager.UpdateInstruction(obstacleInstruction);
        uiManager.ShowFinalObstacleKeyPanel();
        obstacleActivated = true;
    }

    void PopButtons()
    {
        for (int i = 0; i < doorButtons.Length; i++)
        {
            FinalDoorButton doorButton = doorButtons[i] as FinalDoorButton;
            doorButton.PopButton();
        }
    }

    // Pushes the buttons in to show player has completed puzzle
    void PushButtons()
    {
        for (int i = 0; i < doorButtons.Length; i++)
        {
            FinalDoorButton doorButton = doorButtons[i] as FinalDoorButton;
            doorButton.PushButton();
        }
    }

    void ResetButtons()
    {
        for (int i = 0; i < doorButtons.Length; i++)
        {
            FinalDoorButton doorButton = doorButtons[i] as FinalDoorButton;
            doorButton.ResetButton();
        }
    }

    public void DeactivateAllButtons()
    {
        for (int i = 0; i < doorButtons.Length; i++)
        {
            FinalDoorButton doorButton = doorButtons[i] as FinalDoorButton;
            doorButton.DeactivateButton();
        }
    }

    // Check if answer put is in the desired answers, and add them into the list if it is correct
    public bool CheckButtonAnswer(string text)
    {
        if(text == correctAnswers[0])
        {
            
            correctAnswerSelected = true;
            successAudioManager.PlaySuccessSound();
            return true;
        }

        correctAnswerSelected = false;
        successAudioManager.PlayFailureSound();
        return false;
    }


    public override void InteractWithObstacle(string instruction)
    {
        // Check if door is unlocked properly, then allow door to open when player pushes if door is unlocked

        if (instruction.Equals("PushDoor") && IsAnswersComplete())
        {
            Debug.Log("Door open!");
            StartCoroutine(PlayAnimation());
        }
    }

    // Make changes based on input
    public void UpdateObstacle()
    {
        if(currentIndex == initialKeys.Count - 1 && correctAnswerSelected)
        {
            sequenceCompleted = true;
            PushButtons();
            uiManager.UpdateInstruction(completeText);
            uiManager.FadeQuestion();
            // To show whole string
            GenerateFinalDisplayedKeyString(currentIndex + 1);
            //Update score..
            if (!wrongAnswerSelected)
            {
                scoreManager.UpdateScore(3);
            }
            else
            {
                scoreManager.UpdateScore(4);
            }
        }
        else
        {
            StartCoroutine(SwitchKeysOnButton());
        }
        

    }

    IEnumerator PlayAnimation()
    {
        anim.SetTrigger("PushDoor");
        yield return new WaitForSeconds(animationTime);
        isCleared = true;
        uiManager.FadeInstruction();
        uiManager.FadeFinalObstacleKeyPanel();

    }

    IEnumerator SwitchKeysOnButton()
    {
        // Push button back in?
        PushButtons();
        yield return new WaitForSeconds(animationTime);

        // Reinitialise the keys on the buttons
        if (correctAnswerSelected)
        {
            currentIndex++;
        }
        else
        {
            currentIndex = 0;
            wrongAnswerSelected = true;
        }
        Initialise(currentIndex);
        GenerateFinalDisplayedKeyString(currentIndex);
        // Reset buttons and pop them back out
        ResetButtons();

        PopButtons();
    }

    bool IsAnswersComplete()
    {
        return sequenceCompleted;
    }

    void GenerateCorrectAnswers(int currentIndex)
    {
        correctAnswers = new List<string>();
        correctAnswers.Add(initialKeys[currentIndex]);
    }

    public override void UpdateInstructionForObstacle()
    {
        if (!obstacleActivated)
        {
            uiManager.UpdateInstruction(startingInstruction);
        }
        

    }

    void ResetObstacle()
    {
        Initialise(currentIndex);
    }
}
