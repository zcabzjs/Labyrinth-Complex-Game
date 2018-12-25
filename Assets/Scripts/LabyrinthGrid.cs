using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabyrinthGrid : MonoBehaviour {

    public int direction = 0;
    public int toDirection = -1; 
    public bool visited = false;
    public bool isCorner = false;


    public LabyrinthGrid()
    {
        
    }
}
