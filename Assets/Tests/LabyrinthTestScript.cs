using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class LabyrinthTestScript
{

    [Test]
    public void Labyrinth_Not_Null_Test() {
        var labyrinth = GameObject.Find("Labyrinth");
        // Use the Assert class to test conditions.
        Assert.AreNotEqual(null, labyrinth);
    }

    [Test]
    public void Labyrinth_Has_LabyrinthNavigation_Component_Test()
    {
        var labyrinth = GameObject.Find("Labyrinth").GetComponent<LabyrinthNavigation>();
        Assert.AreNotEqual(null, labyrinth);
    }

    [Test]
    public void Labyrinth_Path_Not_Zero_Test()
    {
        GameObject.Find("KeyManager").GetComponent<KeyManager>().GenerateKeys();
        var labyrinth = GameObject.Find("Labyrinth").GetComponent<LabyrinthNavigation>();
        labyrinth.InitiateLabyrinth();
        Assert.Greater(labyrinth.GetPathLength(), 0);
    }

    [Test]
    public void Labyrinth_Waypoints_Not_Empty_Test()
    {
        GameObject.Find("KeyManager").GetComponent<KeyManager>().GenerateKeys();
        var labyrinth = GameObject.Find("Labyrinth").GetComponent<LabyrinthNavigation>();
        labyrinth.InitiateLabyrinth();
        Assert.Greater(labyrinth.GetWayPoints().Count, 0);
    }

    [Test]
    public void Labyrinth_Waypoints_More_Than_PathLength_Test()
    {
        GameObject.Find("KeyManager").GetComponent<KeyManager>().GenerateKeys();
        var labyrinth = GameObject.Find("Labyrinth").GetComponent<LabyrinthNavigation>();
        labyrinth.InitiateLabyrinth();
        int pathLength = labyrinth.GetPathLength();
        int numberOfWaypoints = labyrinth.GetLengthOfLabyrinth();
        // Each path length produces 2 waypoints    + 1 for the treasure room
        Assert.Greater(numberOfWaypoints, 2 * pathLength);
    }

    [Test]
    public void Labyrinth_Number_Of_Obstacles_Generated_Correct_Test()
    {
        GameObject.Find("KeyManager").GetComponent<KeyManager>().GenerateKeys();
        var labyrinth = GameObject.Find("Labyrinth").GetComponent<LabyrinthNavigation>();
        labyrinth.InitiateLabyrinth();
        int numberOfObstacles = labyrinth.GetNumberOfObstacles();
        LabyrinthGrid[,] testLabyrinthGrids = labyrinth.labyrinthGrids;
        int numberOfObstaclesGenerated = 0;
        for(int i = 0; i < testLabyrinthGrids.GetLength(0); i++)
        {
            for(int j = 0; j < testLabyrinthGrids.GetLength(1); j++)
            {
                if(testLabyrinthGrids[i,j] != null)
                {
                    if(testLabyrinthGrids[i,j].obstacle != null)
                    {
                        numberOfObstaclesGenerated++;
                    }
                    
                }
            }
        }
        // Extra 2 obstacles, are the final obstacle and the starting door obstacle
        Assert.AreEqual(numberOfObstaclesGenerated, numberOfObstacles + 2);
    }

    [Test]
    public void Labyrinth_Grid_Generated_On_Waypoints()
    {
        GameObject.Find("KeyManager").GetComponent<KeyManager>().GenerateKeys();
        var labyrinth = GameObject.Find("Labyrinth").GetComponent<LabyrinthNavigation>();
        labyrinth.InitiateLabyrinth();
        bool valid = true;
        var waypoints = labyrinth.GetWayPoints();
        var testLabyrinthGrids = labyrinth.labyrinthGrids;
        for(int i = 0; i < waypoints.Count; i++)
        {
            if(testLabyrinthGrids[waypoints[i].point.Z, waypoints[i].point.X] == null)
            {
                valid = false;
            }
        }
        Assert.AreEqual(true, valid);
    }

}
