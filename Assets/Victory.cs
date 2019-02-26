using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victory : MonoBehaviour {

    public GameObject victoryUI;

    public UIManager uiManager;

	// Use this for initialization
	public void PlayerVictory(float timeTaken)
    {
        // Set score maybe...

        // Set time maybe...
        uiManager.SetEndTime(timeTaken);
        Animator anim = victoryUI.GetComponent<Animator>();
        anim.SetTrigger("playerVictory");
    }
}
