using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabyrinthGrid : MonoBehaviour {

    //public int direction = 0;
    //public int toDirection = -1; 
    public bool visited = false;
    //public bool isCorner = false;
    public Obstacle obstacle = null;

    public bool IsObstructed()
    {
        if(obstacle == null)
        {
            return false;
        }
        else
        {
            return !obstacle.isCleared;
        }
    }

    public LabyrinthGrid()
    {
        
    }

    public void InteractWithObstacle(string instruction)
    {
        if(obstacle == null)
        {
            Debug.Log("Nothing to interact with");
        
        }
        else
        {
            obstacle.InteractWithObstacle(instruction);
        }

    }

    public void UpdateUIForObstacle()
    {
        if(obstacle == null)
        {
            return;
        }
        else
        {
            obstacle.UpdateInstructionForObstacle();
            // Update UI of obstacle
        }
    }
}
