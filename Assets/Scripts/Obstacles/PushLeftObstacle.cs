using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushLeftObstacle : Obstacle {

    Animator anim;
    public bool animationPlaying;
    public float animationTime = 1f;
    UIManager uiManager;
    ScoreManager scoreManager;
    void Start()
    {
        anim = GetComponent<Animator>();
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        scoreManager = GameObject.Find("Score Manager").GetComponent<ScoreManager>();
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
        scoreManager.UpdateScore(5);
        uiManager.FadeInstruction();
    }

    public override void UpdateInstructionForObstacle()
    {
        uiManager.UpdateInstruction("Use your right hand to swipe left.");
    }
}
