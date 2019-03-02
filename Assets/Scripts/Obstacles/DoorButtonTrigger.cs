using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButtonTrigger : MonoBehaviour {

    bool triggerActivated = true;

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
            DoorButton doorButton = GetComponentInParent<DoorButton>();
            if(doorButton != null)
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
    }
}
