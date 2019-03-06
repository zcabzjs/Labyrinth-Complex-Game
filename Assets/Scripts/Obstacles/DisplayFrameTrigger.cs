using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayFrameTrigger : MonoBehaviour {

    bool triggerActivated = true;
    // Use this for initialization

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player") && triggerActivated)
        {
            DisplayFrame frame = GetComponentInParent<DisplayFrame>();
            frame.ChooseFrame();
            triggerActivated = false;
        }
    }
}
