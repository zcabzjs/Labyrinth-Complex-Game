using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour {

    public LabyrinthNavigation labyrinth;

    Vector3 playerCurrentPosition;
    int playerCurrentRotation;

    PlayerMovement playerMovement;

    PlayerGestureListener gestureListener;
    // Use this for initialization
    void Start () {
        // get the gestures listener
        gestureListener = PlayerGestureListener.Instance;

        playerMovement = GetComponent<PlayerMovement>();

    }
	
	// Update is called once per frame
	void Update () {
        if (gestureListener.IsSwipeLeft())
        {
            updatePlayerPositionAndRotation();
            labyrinth.InteractWithLabyrinth(convertDirectionToVector(playerCurrentPosition, playerCurrentRotation), "SwipeLeft");
        }
        else if (gestureListener.IsSwipeRight())
        {
            updatePlayerPositionAndRotation();
            labyrinth.InteractWithLabyrinth(convertDirectionToVector(playerCurrentPosition, playerCurrentRotation), "SwipeRight");
        }
        else if (gestureListener.IsPush())
        {
            updatePlayerPositionAndRotation();
            labyrinth.InteractWithLabyrinth(convertDirectionToVector(playerCurrentPosition, playerCurrentRotation), "PushDoor");
        }
        else if (gestureListener.IsSwipeUp())
        {
            labyrinth.InteractWithLabyrinth(convertDirectionToVector(playerCurrentPosition, playerCurrentRotation), "SwipeUp");
        }
	}

    private Vector3 convertDirectionToVector(Vector3 playerCurrentPosition, int direction)
    {
        switch (direction)
        {
            case 0:
                return playerCurrentPosition + Vector3.forward;
            case 1:
                return playerCurrentPosition + Vector3.right;
            case 2:
                return playerCurrentPosition + Vector3.back;
            case 3:
                return playerCurrentPosition + Vector3.left;
            default:
                Debug.Log("FIX ME: in convertDirectionToVector direction error");
                return new Vector3(0, 0, 0);
        }
    }

    private void updatePlayerPositionAndRotation()
    {
        playerCurrentRotation = playerMovement.playerCurrentRotation;
        playerCurrentPosition = playerMovement.desiredPosition;
    }
}
