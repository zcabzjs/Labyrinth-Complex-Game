using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StartingDoorObstacle: Obstacle
{
    Animator doorAnim;
    public bool animationPlaying;
    public float animationTime = 1f;
    
    LevelManager levelManager;
    UIManager uiManager;

    void Start()
    {
        doorAnim = GetComponentInChildren<Animator>();
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        levelManager = GameObject.Find("Level Manager").GetComponent<LevelManager>();
    }

    public override void InteractWithObstacle(string instruction)
    {
        if (instruction.Equals("PushDoor"))
        {
            // Play animation of thing moving to the left, and then destroyed...
            StartCoroutine(PlayAnimation());
            uiManager.FadeTitleAndKey();
            uiManager.FadeInstruction();
            levelManager.SetStartTime();
        }
    }

    IEnumerator PlayAnimation()
    {
        doorAnim.SetTrigger("PushDoor");
        yield return new WaitForSeconds(animationTime);
        isCleared = true;
    }


    public override void UpdateInstructionForObstacle()
    {
        throw new System.NotImplementedException();
    }
}
