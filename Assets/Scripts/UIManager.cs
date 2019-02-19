using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public TextMeshProUGUI instructionText;

    public Slider progressSlider;

    public LevelManager levelManager;

    float TimeTakenToCompleteLevel;
    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpdateProgressSliderRanges(int minValue, int maxValue)
    {
        progressSlider.minValue = minValue;
        progressSlider.maxValue = maxValue;
    }

    public void UpdateProgressSliderValue(int value)
    {
        progressSlider.value = value;
        if(value >= progressSlider.maxValue)
        {
            TimeTakenToCompleteLevel = levelManager.GetTimeTakenToCompleteLevel();
            // Do some UI stuff here to show that player has reached treasure room
        }
    }

    public void UpdateInstruction(string text)
    {
        instructionText.enabled = true;
        instructionText.color = new Color(instructionText.color.r, instructionText.color.g, instructionText.color.b, 1);
        instructionText.text = text;
        // Do animations of text here?
    }

    public void FadeInstruction()
    {
        StartCoroutine(FadeTextToZeroAlpha(0.2f, instructionText));
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
}
