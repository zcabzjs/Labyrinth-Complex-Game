using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    public LabyrinthNavigation labyrinth;

    public KeyManager keyManager;

    public Victory victoryManager;

    // Keep track of time here..
    public float timeToCompleteLevel;
    float startTime;
    float endTime;
	// Use this for initialization
	void Start () {
        keyManager.GenerateKeys();
        labyrinth.InitiateLabyrinth();
	}

    public void SetStartTime()
    {
        startTime = Time.timeSinceLevelLoad;
    }

    public float GetTimeTakenToCompleteLevel()
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
        victoryManager.PlayerVictory();
    }
}
