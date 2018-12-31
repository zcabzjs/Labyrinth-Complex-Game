using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour {

    public LabyrinthNavigation labyrinth;

    Vector3 playerCurrentPosition;
    int playerCurrentRotation;

    PlayerMovement playerMovement;
    // Use this for initialization
    void Start () {
        playerMovement = GetComponent<PlayerMovement>();

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            updatePlayerPositionAndRotation();
            labyrinth.InteractWithLabyrinth(convertDirectionToVector(playerCurrentPosition, playerCurrentRotation), "PushLeft");
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            updatePlayerPositionAndRotation();
            labyrinth.InteractWithLabyrinth(convertDirectionToVector(playerCurrentPosition, playerCurrentRotation), "PushRight");
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            updatePlayerPositionAndRotation();
            labyrinth.InteractWithLabyrinth(convertDirectionToVector(playerCurrentPosition, playerCurrentRotation), "PushDoor");
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
