using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victory : MonoBehaviour {

    public GameObject victoryUI;

	// Use this for initialization
	public void PlayerVictory()
    {
        // Set score maybe...
        // Set time maybe...
        Animator anim = victoryUI.GetComponent<Animator>();
        anim.SetTrigger("playerVictory");
    }
}
