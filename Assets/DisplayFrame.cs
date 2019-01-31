using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayFrame : MonoBehaviour {

    TextMeshPro displayText;

    Animator anim;

    Light light;



    bool selected = false;

    // Use this for initialization
    void Start () {
        //anim = GetComponent<Animator>();
        light = GetComponentInChildren<Light>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetDisplayText(string text)
    {
        displayText = GetComponentInChildren<TextMeshPro>();
        displayText.text = text;
    }

    public void ChooseFrame()
    {
        if (!selected)
        {
            selected = true;
            // Check with the obstacle
            ThreeChooseOneObstacle obstacle = GetComponentInParent<ThreeChooseOneObstacle>();

            // Enable light and change color accordingly
            light.enabled = true;
            light.color = obstacle.CheckAnswer(displayText.text) ? Color.green : Color.red;

        }
    }
}
