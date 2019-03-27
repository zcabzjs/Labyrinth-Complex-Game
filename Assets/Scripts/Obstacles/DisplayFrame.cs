using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayFrame : MonoBehaviour {

    TextMeshPro displayText;

    Animator anim;

    Light frameLight;



    bool selected = false;

    // Use this for initialization
    void Start () {
        //anim = GetComponent<Animator>();
        frameLight = GetComponentInChildren<Light>();
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
            Obstacle obstacle = GetComponentInParent<Obstacle>();

            // Enable light and change color accordingly
            frameLight.enabled = true;
            frameLight.color = obstacle.CheckAnswer(displayText.text) ? Color.green : Color.red;

        }
    }
}
