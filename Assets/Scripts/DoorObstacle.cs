using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorObstacle : Obstacle {

    Animator anim;
    public bool animationPlaying;
    public float animationTime = 1f;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public override void InteractWithObstacle(string instruction)
    {
        if (instruction.Equals("PushDoor"))
        {
            // Play animation of thing moving to the left, and then destroyed...
            StartCoroutine(PlayAnimation());
        }
    }

    IEnumerator PlayAnimation()
    {
        anim.SetTrigger("PushDoor");
        yield return new WaitForSeconds(animationTime);
        isCleared = true;
    }
}
