using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushLeftObstacle : Obstacle {

    Animator anim;
    public bool animationPlaying;
    public float animationTime = 1f;

    void Start()
    {
        anim = GetComponent<Animator>();
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
        throw new System.NotImplementedException();
    }
}
