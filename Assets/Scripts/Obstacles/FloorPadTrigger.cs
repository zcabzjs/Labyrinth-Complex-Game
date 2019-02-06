using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorPadTrigger : MonoBehaviour {

    bool floorPadTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player") && !floorPadTriggered)
        {
            FloorPad floorPad = GetComponentInParent<FloorPad>();
            floorPad.FloorPadPressed();
            floorPadTriggered = true;
            Obstacle obstacle = GetComponentInParent<Obstacle>();
            obstacle.ActivateObstacle();
            Debug.Log("Floor Pad is pressed");
        }
    }
}
