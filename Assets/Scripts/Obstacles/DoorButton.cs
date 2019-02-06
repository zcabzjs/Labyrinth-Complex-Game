using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DoorButton : MonoBehaviour {

    TextMeshPro buttonText;

    Animator anim;

    bool clicked = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void DoorButtonPressed()
    {
        // Check if button can still be activated
        if (!clicked)
        {
            anim.SetTrigger("pressButton");
            clicked = true;
            DoorButtonTrigger doorButtonTrigger = GetComponentInChildren<DoorButtonTrigger>();
            doorButtonTrigger.DeactivateTrigger();
            // Check with door to see if the input is correct or wrong
            DoorObstacleWithButton doorObstacleWithButton = GetComponentInParent<DoorObstacleWithButton>();
            if (doorObstacleWithButton.CheckButtonAnswer(buttonText.text))
            {
                GameObject indicator = transform.GetChild(0).GetChild(0).gameObject;
                indicator.SetActive(true);
                Debug.Log("Returns true");

            }
            else
            {
                GameObject indicator = transform.GetChild(0).GetChild(1).gameObject;
                indicator.SetActive(true);
                Debug.Log("Returns false");
            }
        }

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
