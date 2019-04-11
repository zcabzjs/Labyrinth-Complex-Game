using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    // Instruction UI
    public Image instructionFrame;
    public Image instructionImage;
    public TextMeshProUGUI instructionText;

    // Title UI
    public Image titleFrame;
    public Image titleImage;
    public TextMeshProUGUI titleText;

    // UI for keys
    public Image displayKeyImage;
    public TextMeshProUGUI displayText;

    // Question UI
    public Image questionFrame;
    public Image questionImage;
    public TextMeshProUGUI questionText;

    // UI for final obstacle
    public Image finalObstacleKeyFrame;
    public Image finalObstacleKeyImage;
    public TextMeshProUGUI finalObstacleKeyText;

    // Time text
    public TextMeshProUGUI timeText;

    // Score text
    public TextMeshProUGUI scoreText;

    public Slider progressSlider;

    public LevelManager levelManager;


    float fadeTextTime = 0.2f;
    float TimeTakenToCompleteLevel;
    // Use this for initialization
    void Start () {
        // Fade question panel at start of game?
        questionFrame.color = new Color(questionFrame.color.r, questionFrame.color.g, questionFrame.color.b, 0);
        questionImage.color = new Color(questionImage.color.r, questionImage.color.g, questionImage.color.b, 0);
        questionText.color = new Color(questionText.color.r, questionText.color.g, questionText.color.b, 0);

        // Fade final obstacle panel at start of game
        finalObstacleKeyFrame.color = new Color(finalObstacleKeyFrame.color.r, finalObstacleKeyFrame.color.g, finalObstacleKeyFrame.color.b, 0);
        finalObstacleKeyImage.color = new Color(finalObstacleKeyImage.color.r, finalObstacleKeyImage.color.g, finalObstacleKeyImage.color.b, 0);
        finalObstacleKeyText.color = new Color(finalObstacleKeyText.color.r, finalObstacleKeyText.color.g, finalObstacleKeyText.color.b, 0);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShowFinalObstacleKeyPanel()
    {
        finalObstacleKeyFrame.enabled = true;
        finalObstacleKeyImage.enabled = true;
        finalObstacleKeyText.enabled = true;

        finalObstacleKeyFrame.color = new Color(finalObstacleKeyFrame.color.r, finalObstacleKeyFrame.color.g, finalObstacleKeyFrame.color.b, 1);
        finalObstacleKeyImage.color = new Color(finalObstacleKeyImage.color.r, finalObstacleKeyImage.color.g, finalObstacleKeyImage.color.b, 1);
        finalObstacleKeyText.color = new Color(finalObstacleKeyText.color.r, finalObstacleKeyText.color.g, finalObstacleKeyText.color.b, 1);
    }

    public void FadeFinalObstacleKeyPanel()
    {
        StartCoroutine(FadeImage(fadeTextTime, finalObstacleKeyFrame));
        StartCoroutine(FadeImage(fadeTextTime, finalObstacleKeyImage));
        StartCoroutine(FadeTextToZeroAlpha(fadeTextTime, finalObstacleKeyText));
    }

    public void UpdateFinalObstacleKey(string text)
    {
        finalObstacleKeyText.text = text;
    }

    public void UpdateQuestion(string text)
    {
        questionFrame.enabled = true;
        questionImage.enabled = true;
        questionFrame.color = new Color(questionFrame.color.r, questionFrame.color.g, questionFrame.color.b, 1);
        questionImage.color = new Color(questionImage.color.r, questionImage.color.g, questionImage.color.b, 1);
        questionText.enabled = true;
        questionText.color = new Color(questionText.color.r, questionText.color.g, questionText.color.b, 1);
        questionText.text = text;
    }

    public void FadeQuestion()
    {
        StartCoroutine(FadeImage(fadeTextTime, questionFrame));
        StartCoroutine(FadeImage(fadeTextTime, questionImage));
        StartCoroutine(FadeTextToZeroAlpha(fadeTextTime, questionText));
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
            // Do some UI stuff here to show that player has reached treasure room

            levelManager.PlayerVictory();
        }
    }

    public void UpdateInstruction(string text)
    {
        instructionFrame.enabled = true;
        instructionImage.enabled = true;
        instructionFrame.color = new Color(instructionFrame.color.r, instructionFrame.color.g, instructionFrame.color.b, 1);
        instructionImage.color = new Color(instructionImage.color.r, instructionImage.color.g, instructionImage.color.b, 1);
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

    public void SetEndTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60F);
        int seconds = Mathf.FloorToInt(time - minutes * 60);
        string niceTime = string.Format("{0:0}m{1:00}s", minutes, seconds);
        timeText.text = niceTime;
    }

    public void SetEndScore(int score)
    {
        scoreText.text = score.ToString();
    }
}
