using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayFrame : MonoBehaviour {

    TextMeshPro displayText;

    Animator anim;

    bool selected = false;

    // Use this for initialization
    void Start () {
        //anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetDisplayText(string text)
    {
        displayText = GetComponentInChildren<TextMeshPro>();
        displayText.text = text;
    }
}
