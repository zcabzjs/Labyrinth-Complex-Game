using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushLeftObstacle : Obstacle {

    Animator anim;
    public bool animationPlaying;
    public float animationTime = 1f;
    UIManager uiManager;
    void Start()
    {
        anim = GetComponent<Animator>();
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
    }

    public override void InteractWithObstacle(string instruction)
    {
        if (instruction.Equals("SwipeLeft")){
            // Play animation of thing moving to the left, and then destroyed...
            StartCoroutine(PlayAnimation());
        }
        
    }

    IEnumerator PlayAnimation()
    {
        anim.SetTrigger("PushLeft");
        yield return new WaitForSeconds(animationTime);
        isCleared = true;
    }

    public override void UpdateInstructionForObstacle()
    {
        uiManager.UpdateInstruction("Swipe in the direction of the arrow.");
    }
}
