using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    public LabyrinthNavigation labyrinth;

    public KeyManager keyManager;

	// Use this for initialization
	void Start () {
        keyManager.GenerateKeys();
        labyrinth.InitiateLabyrinth();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
