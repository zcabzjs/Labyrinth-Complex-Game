using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StartingDoorObstacle: Obstacle
{

    Animator anim;
    public bool animationPlaying;
    public float animationTime = 1f;
    public TextMeshProUGUI displayText;
    public TextMeshProUGUI titleText;
    public float fadeTextTime = 1.5f;
    public LevelManager levelManager;


    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        titleText = GameObject.Find("Title Text").GetComponent<TextMeshProUGUI>();
        displayText = GameObject.Find("Display Key Text").GetComponent<TextMeshProUGUI>();
        levelManager = GameObject.Find("Level Manager").GetComponent<LevelManager>();
    }

    public override void InteractWithObstacle(string instruction)
    {
        if (instruction.Equals("PushDoor"))
        {
            // Play animation of thing moving to the left, and then destroyed...
            StartCoroutine(PlayAnimation());
            StartCoroutine(FadeTextToZeroAlpha(fadeTextTime, displayText));
            StartCoroutine(FadeTextToZeroAlpha(fadeTextTime, titleText));
            levelManager.SetStartTime();
        }
    }

    IEnumerator PlayAnimation()
    {
        anim.SetTrigger("PushDoor");
        yield return new WaitForSeconds(animationTime);
        isCleared = true;
    }

    public IEnumerator FadeTextToZeroAlpha(float t, TextMeshProUGUI i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
        i.enabled = false;
        
    }

    public override void UpdateInstructionForObstacle()
    {
        throw new System.NotImplementedException();
    }
}
