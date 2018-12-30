using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabyrinthGrid : MonoBehaviour {

    //public int direction = 0;
    //public int toDirection = -1; 
    public bool visited = false;
    //public bool isCorner = false;
    public Obstacle obstacle = null;

    public bool isObstructed = false;

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
            if (obstacle.InteractWithObstacle(instruction))
            {
                isObstructed = false;
            }
            else
            {
                Debug.Log("Wrong movement");
            }
        }

    }
}
