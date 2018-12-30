using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushLeftObstacle : Obstacle {

    Animator anim;
    public bool animationPlaying;
    public float animationTime = 1f;
    public override void InteractWithObstacle(string instruction)
    {
        if (instruction.Equals("PushLeft")){
            // Play animation of thing moving to the left, and then destroyed...
            anim = GetComponent<Animator>();
            StartCoroutine(PlayAnimation());
        }
        
    }

    IEnumerator PlayAnimation()
    {
        anim.SetTrigger("PushLeft");
        yield return new WaitForSeconds(animationTime);
        isCleared = true;
    }
}
