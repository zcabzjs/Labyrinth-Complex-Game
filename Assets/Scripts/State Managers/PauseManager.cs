using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour {

    bool isPaused = false;
    public GameObject PauseUI;
    AudioSource backgroundAudio;
    Victory victoryManager;
	// Use this for initialization
	void Start () {
        backgroundAudio = GameObject.Find("Level Manager").GetComponent<AudioSource>();
        victoryManager = GameObject.Find("Victory Manager").GetComponent<Victory>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp("escape") && !victoryManager.victoryAchieved)
        {
            if (!isPaused)
            {
                 PauseGame();
            }
            else ResumeGame();
        }
    }

    void PauseGame()
    {
        Time.timeScale = 0f;
        backgroundAudio.Pause();
        PauseUI.SetActive(true);
        isPaused = true;
    }

    public void ResumeGame()
    {
        PauseUI.SetActive(false);
        backgroundAudio.Play();
        Time.timeScale = 1f;
        isPaused = false;
    }
}
