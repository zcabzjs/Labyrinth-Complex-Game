using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReachAndTouchObstacle : Obstacle {

    string completeText = "Swipe left to proceed.";

    UIManager uiManager;

    DisplayFrame[] frames;

    GameObject arrow;

    public override void InteractWithObstacle(string instruction)
    {
        throw new System.NotImplementedException();
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

    }
    // Use this for initialization
    void Start () {
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        Initialise();
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    // Function is run when object is initialised
    void Initialise()
    {
        frames = gameObject.GetComponentsInChildren<DisplayFrame>();
        for(int i = 0; i < frames.Length; i++)
        {
            // Set the display frames as disabled
            frames[i].gameObject.SetActive(false);
        }

        arrow = this.transform.Find("Arrow").gameObject;
        arrow.SetActive(false);
    }
}
