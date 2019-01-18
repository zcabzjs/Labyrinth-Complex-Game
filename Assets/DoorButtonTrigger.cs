using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButtonTrigger : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            DoorButton doorButton = GetComponentInParent<DoorButton>();
            doorButton.DoorButtonPressed();
            
        }
    }
}
