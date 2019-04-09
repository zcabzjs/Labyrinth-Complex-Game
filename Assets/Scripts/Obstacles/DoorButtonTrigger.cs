using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorButtonTrigger : MonoBehaviour {

    bool triggerActivated = true;

    float activationTime = 1f;
    float currentTime;

    bool timed = false;

    Canvas canvas;
    Slider slider;

    void Start()
    {
        canvas = GetComponentInChildren<Canvas>();
        slider = GetComponentInChildren<Slider>();
    }

    private void Update()
    {
        if(timed && currentTime < activationTime && triggerActivated)
        {
            currentTime += Time.deltaTime;
            //Update slider with currentTime;
            //Debug.Log("Current time: " + currentTime);
            slider.value = currentTime;
        }
    }

    public void DeactivateTrigger()
    {
        triggerActivated = false;
    }

    public void ActivateTrigger()
    {
        triggerActivated = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player") && triggerActivated)
        {
            StartCoroutine(StartTimer());
        }
    }

    IEnumerator StartTimer()
    {
        //Activate slider        
        canvas.enabled = true;

        currentTime = 0f;
        //Set slider max and min values
        slider.maxValue = activationTime;
        slider.minValue = currentTime;

        timed = true;
        yield return new WaitForSeconds(activationTime);
        
        PressButton();
        ResetTimer();
    }

    private void PressButton()
    {
        DoorButton doorButton = GetComponentInParent<DoorButton>();
        if (doorButton != null)
        {
            doorButton.DoorButtonPressed();
        }
        else
        {
            // Same door trigger  used for final door obstacle
            FinalDoorButton finalDoorButton = GetComponentInParent<FinalDoorButton>();
            finalDoorButton.DoorButtonPressed();
        }
    }

    void ResetTimer()
    {
        currentTime = 0f;
        slider.value = currentTime;
        canvas.enabled = false;
        timed = false;

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Player") && triggerActivated)
        {
            ResetTimer();

            StopAllCoroutines();
        }
    }
}
