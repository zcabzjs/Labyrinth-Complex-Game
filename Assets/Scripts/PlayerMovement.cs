using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float timeBetweenMoves = 0.3333f;
    private float timestamp;
    public float interpolationSpeed = 10.0F;
    public Vector3 desiredPosition;

    public LabyrinthNavigation labyrinth;

    //Rotation values: 0 for 0, 1 for 90, 2 for 180, 3 for 270 degrees
    public int playerCurrentRotation = 0;

    Vector3 checkPoint;

    // Use this for initialization
    void Start()
    {
        desiredPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        ArrowKeyRotation();
        transform.position = Vector3.Lerp(transform.position, desiredPosition, interpolationSpeed * Time.deltaTime);
        if (desiredPosition == checkPoint)
        {
            Debug.Log("Checkpoint arrived!");
            // Play animation that checks stuff
            // Check final answer with answer collected from player

        }
    }

    void ArrowKeyRotation()
    {
        if (Time.time >= timestamp)
        {
            if (Input.GetKey("up"))
            {
                UpdateRotationAndDesiredPosition();
                timestamp = Time.time + timeBetweenMoves;
            }
            if (Input.GetKey("down"))
            {
                playerCurrentRotation = (playerCurrentRotation + 2) % 4;
                UpdateRotationAndDesiredPosition();
                timestamp = Time.time + timeBetweenMoves;
            }
            else if (Input.GetKey("left"))
            {
                playerCurrentRotation = (playerCurrentRotation + 3) % 4;
                UpdateRotationAndDesiredPosition();
                timestamp = Time.time + timeBetweenMoves;
            }
            else if (Input.GetKey("right"))
            {
                playerCurrentRotation = (playerCurrentRotation + 1) % 4;
                UpdateRotationAndDesiredPosition();
                timestamp = Time.time + timeBetweenMoves;
            }

        }
    }

    void UpdateRotationAndDesiredPosition()
    {
        FixPlayerRotation();
        UpdateDesiredPosition();
    }

    void FixPlayerRotation()
    {
        transform.eulerAngles = new Vector3(0, 90 * playerCurrentRotation, 0);
    }

    void UpdateDesiredPosition()
    {
        switch (playerCurrentRotation)
        {
            case 0:
                if(labyrinth.CanWalk(desiredPosition + Vector3.forward))
                {
                    desiredPosition += Vector3.forward;
                }
                break;
            case 1:
                if (labyrinth.CanWalk(desiredPosition + Vector3.right))
                {
                    desiredPosition += Vector3.right;
                }
                break;
            case 2:
                if (labyrinth.CanWalk(desiredPosition + Vector3.back))
                {
                    desiredPosition += Vector3.back;
                }         
                break;
            case 3:
                if (labyrinth.CanWalk(desiredPosition + Vector3.left))
                {
                    desiredPosition += Vector3.left;
                }
                break;
            default:
                Debug.Log("FIX ME. Direction doesn't exist.");
                break;
        }

    }
}
