using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victory : MonoBehaviour {

    public GameObject victoryUI;

    public UIManager uiManager;

    public bool victoryAchieved = false;

	// Use this for initialization
	public void PlayerVictory(float timeTaken, int score)
    {
        // Set score maybe...
        uiManager.SetEndScore(score);
        // Set time maybe...
        uiManager.SetEndTime(timeTaken);
        Animator anim = victoryUI.GetComponent<Animator>();
        anim.SetTrigger("playerVictory");
        victoryAchieved = true;
    }
}
