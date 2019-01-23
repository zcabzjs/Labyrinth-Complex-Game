using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButtonTrigger : MonoBehaviour {

    bool triggerActivated = true;

    public void DeactivateTrigger()
    {
        triggerActivated = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player") && triggerActivated)
        {
            DoorButton doorButton = GetComponentInParent<DoorButton>();
            doorButton.DoorButtonPressed();
            
        }
    }
}
