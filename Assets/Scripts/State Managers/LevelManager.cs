using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    public LabyrinthNavigation labyrinth;

    public KeyManager keyManager;

    public ScoreManager scoreManager;

    public Victory victoryManager;

    AudioSource audioSource;

    // Keep track of time here..
    float timeToCompleteLevel;
    float startTime;
    float endTime;
	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
        keyManager.GenerateKeys();
        labyrinth.InitiateLabyrinth();
	}

    public void SetStartTime()
    {
        startTime = Time.timeSinceLevelLoad;
    }

    private float GetTimeTakenToCompleteLevel()
    {
        endTime = Time.timeSinceLevelLoad;
        timeToCompleteLevel = endTime - startTime;
        return timeToCompleteLevel;
    }
    //victory.overallTime.ToString("#.##")
	// Update is called once per frame
	void Update () {
		
	}

    public void PlayerVictory()
    {
        float timeTaken = GetTimeTakenToCompleteLevel();
        int endScore = scoreManager.GetEndScore();
        victoryManager.PlayerVictory(timeTaken, endScore);
        StartCoroutine(FadeMusic(audioSource, 1f));
    }

    IEnumerator FadeMusic(AudioSource audiosource, float fadetime)
    {
        float startVol = audiosource.volume;
        while(audiosource.volume > 0)
        {
            audiosource.volume -= startVol * Time.deltaTime / fadetime;
            yield return null;
        }

        audiosource.Stop();
        audiosource.volume = startVol;
    }
}
