using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public Image instructionFrame;
    public Image instructionImage;

    public Image titleFrame;
    public Image titleImage;
    public Image displayKeyImage;

    public TextMeshProUGUI instructionText;

    public Slider progressSlider;

    public LevelManager levelManager;

    public GameObject titlePanel;

    public TextMeshProUGUI displayText;
    public TextMeshProUGUI titleText;
    float fadeTextTime = 0.2f;
    float TimeTakenToCompleteLevel;
    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void FadeTitleAndKey()
    {
        StartCoroutine(FadeImage(fadeTextTime, titleFrame));
        StartCoroutine(FadeImage(fadeTextTime, titleImage));
        StartCoroutine(FadeImage(fadeTextTime, displayKeyImage));
        StartCoroutine(FadeTextToZeroAlpha(fadeTextTime, displayText));
        StartCoroutine(FadeTextToZeroAlpha(fadeTextTime, titleText));
        
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

            levelManager.PlayerVictory();
        }
    }

    public void UpdateInstruction(string text)
    {
        instructionFrame.enabled = true;
        instructionImage.enabled = true;
        instructionFrame.color = new Color(instructionFrame.color.r, instructionFrame.color.b, instructionFrame.color.g, 1);
        instructionImage.color = new Color(instructionImage.color.r, instructionImage.color.b, instructionImage.color.g, 1);
        instructionText.enabled = true;
        instructionText.color = new Color(instructionText.color.r, instructionText.color.g, instructionText.color.b, 1);
        instructionText.text = text;
        // Do animations of text here?
    }

    public void FadeInstruction()
    {
        StartCoroutine(FadeImage(fadeTextTime, instructionFrame));
        StartCoroutine(FadeImage(fadeTextTime, instructionImage));
        StartCoroutine(FadeTextToZeroAlpha(fadeTextTime, instructionText));
    }

    public IEnumerator FadeImage(float t, Image i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
        i.enabled = false;
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
