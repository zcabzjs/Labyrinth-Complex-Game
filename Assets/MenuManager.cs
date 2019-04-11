using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

    public Toggle EasyDifficulty;
    public Toggle NormalDifficulty;
    public Toggle HardDifficulty;
    
    


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ClickToStart()
    {
        SceneManager.LoadScene(1);
    }

    public void ClickToQuit()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }

    public void SetDifficulty()
    {
        if (EasyDifficulty.isOn)
        {
            PlayerPrefs.SetInt("Difficulty", 0);
        }
        if (NormalDifficulty.isOn)
        {
            PlayerPrefs.SetInt("Difficulty", 1);
        }
        else if (HardDifficulty.isOn)
        {
            PlayerPrefs.SetInt("Difficulty", 2);
        }

    }

    public void ChooseDifficulty() // Turns on the toggle based on Loaded PlayerPrefs
    {
        int difficulty = PlayerPrefs.GetInt("Difficulty", 0);
        if (difficulty == 0) // Use switch case if theres more buttons coming up
        {
            EasyDifficulty.isOn = true;
        }
        else if (difficulty == 1)
        {
            NormalDifficulty.isOn = true;
        }
        else if(difficulty == 2)
        {
            HardDifficulty.isOn = true;
        }
    }
}
