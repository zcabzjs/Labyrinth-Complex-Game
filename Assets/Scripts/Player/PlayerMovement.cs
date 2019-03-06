using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public bool moveWithKinect = true;
    public bool moveWithMouseAndKeyboard = true;
    public float timeBetweenMoves = 0.3333f;
    private float timestamp;
    public float interpolationSpeed = 10.0F;
    public Vector3 desiredPosition;
    AudioSource audioSource;
    public LabyrinthNavigation labyrinth;

    //Rotation values: 0 for 0, 1 for 90, 2 for 180, 3 for 270 degrees
    public int playerCurrentRotation = 0;

    Vector3 checkPoint;
    bool audioPlayed = false;
    PlayerGestureListener gestureListener;
    // Use this for initialization
    void Start()
    {
        desiredPosition = transform.position;
        gestureListener = PlayerGestureListener.Instance;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovementInput();
        transform.position = Vector3.Lerp(transform.position, desiredPosition, interpolationSpeed * Time.deltaTime);
        float dist = Vector3.Distance(desiredPosition, transform.position);
        if(dist > 0.1 && !audioSource.isPlaying)
        {
            if (!audioPlayed)
            {
                audioSource.Play();
                audioPlayed = true;
            }
            else
            {
                audioSource.UnPause();
            }
            
        }
        else
        {
            if (audioSource.isPlaying)
            {
                audioSource.Pause();
            }
        }
    }

    void PlayerMovementInput()
    {
        if (Time.time >= timestamp)
        {
            if ((moveWithKinect && gestureListener.IsRunning())||(moveWithMouseAndKeyboard && Input.GetKeyDown(KeyCode.W)))
            {
                UpdateRotationAndDesiredPosition("up");
                timestamp = Time.time + timeBetweenMoves;
            }
            else if ((moveWithKinect && gestureListener.IsLeftHandRaised()) || (moveWithMouseAndKeyboard && Input.GetKeyDown(KeyCode.A)))
            {

                FixPlayerRotation("left");
                timestamp = Time.time + timeBetweenMoves;
            }
            else if ((moveWithKinect && gestureListener.IsRightHandRaised()) || (moveWithMouseAndKeyboard && Input.GetKeyDown(KeyCode.D)))
            {
                FixPlayerRotation("right");
                timestamp = Time.time + timeBetweenMoves;
            }

        }
    }

    void FixPlayerRotation(string direction)
    {
        switch (direction)
        {
            case "left":
                if (CanRotateInLabyrinth((playerCurrentRotation + 3) % 4))
                {
                    playerCurrentRotation = (playerCurrentRotation + 3) % 4;
                }
                break;
            case "right":
                if (CanRotateInLabyrinth((playerCurrentRotation + 1) % 4))
                {
                    playerCurrentRotation = (playerCurrentRotation + 1) % 4;
                }        
                break;
            case "up":
                break;
            default:
                Debug.Log("Direction is broken in FixPlayerRotation");
                break;
        }
        transform.eulerAngles = new Vector3(0, 90 * playerCurrentRotation, 0);
        UpdateLabyrinthOnPlayerNextObstacle(playerCurrentRotation);

    }

    void UpdateRotationAndDesiredPosition(string direction)
    {
        if (CanNavigateLabyrinth(playerCurrentRotation))
        {
            //FixPlayerRotation(direction);
            switch (playerCurrentRotation)
            {
                case 0:
                    desiredPosition += Vector3.forward;
                    
                    break;
                case 1:
                    desiredPosition += Vector3.right;
                    
                    break;
                case 2:
                    desiredPosition += Vector3.back;
                    
                    break;
                case 3:
                    desiredPosition += Vector3.left;

                    break;
                default:
                    Debug.Log("FIX ME. Direction doesn't exist.");
                    break;
            }
            labyrinth.VisitedGrid(desiredPosition);
            UpdateLabyrinthOnPlayerNextObstacle(playerCurrentRotation);

        }
    }

    private void UpdateLabyrinthOnPlayerNextObstacle(int direction)
    {
        switch (direction)
        {
            case 0:
                labyrinth.UpdateUIForNextObstacle(desiredPosition + Vector3.forward);
                break;
            case 1:
                labyrinth.UpdateUIForNextObstacle(desiredPosition + Vector3.right);
                break;
            case 2:
                labyrinth.UpdateUIForNextObstacle(desiredPosition + Vector3.back);
                break;
            case 3:
                labyrinth.UpdateUIForNextObstacle(desiredPosition + Vector3.left);
                break;
            default:
                Debug.Log("FIX ME UpdateLabyrinthOnPlayerNextObstacle");
                break;
        }
    }

    private bool CanNavigateLabyrinth(int direction)
    {
        switch (direction)
        {
            case 0:
                return labyrinth.CanWalk(desiredPosition + Vector3.forward);
            case 1:
                return labyrinth.CanWalk(desiredPosition + Vector3.right);
            case 2:
                return labyrinth.CanWalk(desiredPosition + Vector3.back);
            case 3:
                return labyrinth.CanWalk(desiredPosition + Vector3.left);
            default:
                Debug.Log("FIX ME CanNavigateLabyrinth");
                break;
        }
        return false;
    }

    private bool CanRotateInLabyrinth(int direction)
    {
        switch (direction)
        {
            case 0:
                return labyrinth.CanRotate(desiredPosition + Vector3.forward);
            case 1:
                return labyrinth.CanRotate(desiredPosition + Vector3.right);
            case 2:
                return labyrinth.CanRotate(desiredPosition + Vector3.back);
            case 3:
                return labyrinth.CanRotate(desiredPosition + Vector3.left);
            default:
                Debug.Log("FIX ME CanRotateInLabyrinth");
                break;
        }
        return false;
    }
}
