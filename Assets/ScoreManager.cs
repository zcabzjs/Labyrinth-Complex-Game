using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public TextMeshProUGUI scorePoints;
    public TextMeshProUGUI scoreMessage;

    int currentScore = 0;
    int displayedScore = 0;

    float scoreMessageFadeTime = 0.2f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        UpdateScoreUI();

    }

    void UpdateScoreUI()
    {
        if (displayedScore < currentScore)
        {
            displayedScore++;
            scorePoints.text = displayedScore.ToString();
            // Play sound when points update
        }
    }
    
    public int GetEndScore()
    {
        return currentScore;
    }

    public void UpdateScore(int scoreStatus)
    {
        UpdateMessage(scoreStatus);
        switch (scoreStatus)
        {
            case 1:
                currentScore += 100;
                break;
            case 2:
                currentScore += 50;
                break;
            default:
                currentScore += 50;
                break;

        }
    }

    void UpdateMessage(int scoreStatus)
    {
        StartCoroutine(ShowScoreMessage(scoreStatus));
        
    }

    public IEnumerator ShowScoreMessage(int scoreStatus)
    {
        string message = "";
        Color scoreColor;
        switch (scoreStatus)
        {
            case 1:
                message = "First try !  Well done !";
                scoreColor = new Color32(113, 255, 0, 255);
                break;
            case 2:
                message = "Good !";
                scoreColor = new Color32(223, 255, 0, 255);
                break;
            default:
                message = "Well done !";
                scoreColor = new Color32(113, 255, 0, 255);
                break;
                
        }

        scoreMessage.color = scoreColor;
        //Debug.Log("RGB values: " + scoreMessage.color.r + ',' + scoreMessage.color.g + ',' + scoreMessage.color.b);
        scoreMessage.text = message;
        // Pause for 2 seconds?
        yield return new WaitForSeconds(2);
        StartCoroutine(FadeTextToZeroAlpha(scoreMessageFadeTime, scoreMessage));

    }

    public IEnumerator FadeTextToZeroAlpha(float t, TextMeshProUGUI i)
    {

        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
        i.text = "";
    }
}
