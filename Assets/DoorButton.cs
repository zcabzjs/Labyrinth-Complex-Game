using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DoorButton : MonoBehaviour {

    TextMeshPro buttonText;

    public void DoorButtonPressed()
    {
        // Check if button can still be activated
        // Check with door to see if the input is correct or wrong
        DoorObstacleWithButton doorObstacleWithButton = GetComponentInParent<DoorObstacleWithButton>();
        doorObstacleWithButton.CheckButtonAnswer(buttonText.text);
        Debug.Log("Pushed " + buttonText.text);
        // If wrong, do something

        // If right, do something
    }

    public void SetButtonText(string text)
    {
        buttonText = GetComponentInChildren<TextMeshPro>();
        buttonText.text = text;
    }

    public void PushButton()
    {
        Animator anim = GetComponent<Animator>();
        anim.SetTrigger("pushButton");
    }

    public void PopButton()
    {
        Animator anim = GetComponent<Animator>();
        anim.SetTrigger("popButton");
    }

}
