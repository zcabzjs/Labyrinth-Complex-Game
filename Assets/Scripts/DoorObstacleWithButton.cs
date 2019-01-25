using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorObstacleWithButton : Obstacle {

    string obstacleInstruction = "Touch the targets with the numbers in the original sequence.";
    string completeText = "Push door to proceed";

    int numberOfButtonsOnDoor = 7; // Number of buttons on door
    int minimumNumberOfCorrectAnswers = 1; // Number of correct answers minimum

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

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        // Call initialise here
        Initialise();
    }
	
	// Update is called once per frame
	void Update () {
	}

    // Initialise the buttons on the door by randomly generating random stuff
    void Initialise()
    {
        GenerateKeyOptions();
        ShuffleChoices();
        SetButtonText();
        GenerateCorrectAnswers();
    }

    // Generate the options to be put on the door
    void GenerateKeyOptions()
    {
        checkedAnswers = new List<string>();
        choicesToPutOnButton = new List<string>();

        keyManager = GameObject.Find("KeyManager").GetComponent<KeyManager>();
        initialKeys = keyManager.initialKeyArray;
        stringsToChooseFrom = keyManager.mixedCharacters;
        for (int i = 0; i < numberOfButtonsOnDoor; i++)
        {
            if (i < minimumNumberOfCorrectAnswers)
            {
                bool isKeyGenerated = false;
                while (!isKeyGenerated)
                {
                    int r = UnityEngine.Random.Range(0, initialKeys.Count);
                    string chosenString = initialKeys[r].ToString();
                    if (!choicesToPutOnButton.Contains(chosenString))
                    {
                        choicesToPutOnButton.Add(chosenString);
                        isKeyGenerated = true;
                    }
                }
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
        doorButtons = GetComponentsInChildren<DoorButton>();
        for(int i = 0; i < doorButtons.Length; i++)
        {
            DoorButton doorButton = doorButtons[i] as DoorButton;
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
    public void ActivateButtons()
    {
        for (int i = 0; i < doorButtons.Length; i++)
        {
            DoorButton doorButton = doorButtons[i] as DoorButton;
            doorButton.PopButton();
        }
    }

    // Pushes the buttons in to show player has completed puzzle
    void PushButtons()
    {
        for (int i = 0; i < doorButtons.Length; i++)
        {
            DoorButton doorButton = doorButtons[i] as DoorButton;
            doorButton.PushButton();
        }
    }

    // Check if answer put is in the desired answers, and add them into the list if it is correct
    public bool CheckButtonAnswer(string text)
    {
        if (correctAnswers.Contains(text) && !checkedAnswers.Contains(text))
        {
            checkedAnswers.Add(text);
            if (IsAnswersComplete())
            {
                PushButtons();
                uiManager.UpdateInstruction(completeText);

            }
            return true;
        }
        return false;
    }

    // Check if answer by player is complete
    bool IsAnswersComplete()
    {
        // Allow door to open if so..
        if(checkedAnswers.Count == correctAnswers.Count)
        {
            return true;
        }
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

    IEnumerator PlayAnimation()
    {
        anim.SetTrigger("PushDoor");
        yield return new WaitForSeconds(animationTime);
        isCleared = true;
        uiManager.FadeInstruction();
    }

    void GenerateCorrectAnswers()
    {
        correctAnswers = new List<string>();
        for (int i = 0; i < choicesToPutOnButton.Count; i++)
        {
            if (initialKeys.Contains(choicesToPutOnButton[i]))
            {
                correctAnswers.Add(choicesToPutOnButton[i]);
            }
        }
    }

    public override void UpdateInstructionForObstacle()
    {
        //Nothing..
        uiManager.UpdateInstruction(obstacleInstruction);
    }
}
