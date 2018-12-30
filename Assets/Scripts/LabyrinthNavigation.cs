using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabyrinthNavigation : MonoBehaviour {

    public char[,] labyrinthArray;

    

    public int pathLength = 8;

    // Wall prefab used to instantiate
    public Transform wallPrefab;
    // Floor prefab used to instantiate
    public Transform floorPrefab;

    public GridPoint startingPoint;

    // Note to self: labyrinthGrids follow a typical array coordinate, so (1,0) is actually 1 on Z axis, and 0 on X axis
    public LabyrinthGrid[,] labyrinthGrids;

    // Note to self: Waypoints follow the Unity coordinates, so (0,1) is actually 0 on X axis and 1 on Z axis
    List<WayPoint> wayPoints;

    // Player prefab to be instantiated
    //public Transform playerPrefab;

    // Use this for initialization
    void Start () {

        int length = UnityEngine.Random.Range(pathLength+5, pathLength + 10);
        int width = UnityEngine.Random.Range(pathLength+5, pathLength + 10);

        startingPoint = new GridPoint(0, 0);

        labyrinthArray = new char[width, length];
        labyrinthGrids = new LabyrinthGrid[width, length];

        GeneratePath();
        GenerateLabyrinth();

        // Instantiate the player at the starting point
        //Instantiate(playerPrefab, new Vector3(startingPoint.Z, 0, startingPoint.X), Quaternion.identity);


        // Code for visualising the labyrinth
        /*string array = "";
        for(int i = width-1; i >=0; i--)
        {
            for(int j = 0; j < length - 1; j++)
            {
                array += labyrinthArray[i, j];
            }
            array += '\n';
        }
        print(array);
        */


        /*for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < length; j++)
            {
                Instantiate(floorPrefab, new Vector3(j, 0, i), Quaternion.identity);
                //Instantiate(floorPrefab, new Vector3(j, 1.7f, i), Quaternion.identity);
                if (labyrinthArray[i, j] == 'x')
                {
                    Instantiate(wallPrefab, new Vector3(j, 1, i), Quaternion.identity);

                }
            }
        }*/

        /*for (int i = -1; i <= width; i++)
        {
            for (int j = -1; j <= length; j++)
            {
                if (i == -1 || i == width)
                {
                    Instantiate(wallPrefab, new Vector3(j, 1, i), Quaternion.identity);
                }
                else if (j == -1 || j == length)
                {
                    Instantiate(wallPrefab, new Vector3(j, 1, i), Quaternion.identity);
                }
            }
        }*/

    }

    private void InitialiseLabyrinth()
    {
        for (int i = 0; i < labyrinthArray.GetLength(0); i++)
        {
            for (int j = 0; j < labyrinthArray.GetLength(1); j++)
            {
                labyrinthArray[i, j] = 'x';
            }
        }
    }
	private bool IsValidForGeneration(int x, int y)
    {
        return IsValid(x, y) && !IsEmpty(x, y);
    }

    private bool IsValid(int x, int y)
    {
        if(x >= labyrinthArray.GetLength(0) || y >= labyrinthArray.GetLength(1))
        {
            return false;
        }
        else if(x < 0 || y < 0)
        {
            return false;
        }
        return true;
    }

    private bool IsEmpty(int x, int y)
    {
        return labyrinthArray[x, y] == 'o';
    }

    private void GeneratePath()
    {
        int currentDirection = 1;
        GridPoint currentPoint;
        bool pathGenerated = false;
        while (!pathGenerated)
        {
            InitialiseLabyrinth();
            currentPoint = new GridPoint(startingPoint.Z, startingPoint.X);
            labyrinthArray[currentPoint.Z, currentPoint.X] = 'o';
            wayPoints = new List<WayPoint>();
            WayPoint startingWayPoint = new WayPoint(startingPoint);
            wayPoints.Add(startingWayPoint);
            for (int i = 0; i < pathLength; i++)
            {
                bool blockGenerated = false;
                while (!blockGenerated)
                {

                    int direction;
                    if (i == 0) {
                        // Setting direction to always go straight on the 1st block
                        direction = 0;
                    }
                    else
                    {
                        direction = GenerateRandomDirection(currentDirection);
                    }
                        
                    switch (direction)
                    {
                        case 0:
                            if (IsValidForGeneration(currentPoint.X + 2, currentPoint.Z))
                            {
                                labyrinthArray[currentPoint.X + 1, currentPoint.Z] = 'o';
                                labyrinthArray[currentPoint.X + 2, currentPoint.Z] = 'o';

                                wayPoints.Add(new WayPoint(new GridPoint(currentPoint.Z, currentPoint.X + 1)));
                                wayPoints.Add(new WayPoint(new GridPoint(currentPoint.Z, currentPoint.X + 2)));

                                blockGenerated = true;
                                currentPoint.X = currentPoint.X + 2;
                            }
                            break;
                        case 1:
                            if (IsValidForGeneration(currentPoint.X, currentPoint.Z + 2))
                            {
                                labyrinthArray[currentPoint.X, currentPoint.Z + 1] = 'o';
                                labyrinthArray[currentPoint.X, currentPoint.Z + 2] = 'o';

                                wayPoints.Add(new WayPoint(new GridPoint(currentPoint.Z + 1, currentPoint.X)));
                                wayPoints.Add(new WayPoint(new GridPoint(currentPoint.Z + 2, currentPoint.X)));

                                blockGenerated = true;
                                currentPoint.Z = currentPoint.Z + 2;
                            }
                            break;
                        case 2:
                            if (IsValidForGeneration(currentPoint.X - 2, currentPoint.Z))
                            {
                                labyrinthArray[currentPoint.X - 1, currentPoint.Z] = 'o';
                                labyrinthArray[currentPoint.X - 2, currentPoint.Z] = 'o';

                                wayPoints.Add(new WayPoint(new GridPoint(currentPoint.Z, currentPoint.X - 1)));
                                wayPoints.Add(new WayPoint(new GridPoint(currentPoint.Z, currentPoint.X - 2)));

                                blockGenerated = true;
                                currentPoint.X = currentPoint.X - 2;
                            }
                            break;
                        case 3:
                            if (IsValidForGeneration(currentPoint.X, currentPoint.Z - 2))
                            {
                                labyrinthArray[currentPoint.X, currentPoint.Z - 1] = 'o';
                                labyrinthArray[currentPoint.X, currentPoint.Z - 2] = 'o';

                                wayPoints.Add(new WayPoint(new GridPoint(currentPoint.Z - 1, currentPoint.X)));
                                wayPoints.Add(new WayPoint(new GridPoint(currentPoint.Z - 2, currentPoint.X)));

                                blockGenerated = true;
                                currentPoint.Z = currentPoint.Z - 2;

                            }
                            break;
                        default:
                            Debug.Log("Something wrong with the directions during path generation.");
                            break;
                    }
                }
                if(i == pathLength - 1)
                {
                    pathGenerated = true;
                }
                if (!IsValidForGeneration(currentPoint.X, currentPoint.Z + 2) && !IsValidForGeneration(currentPoint.X - 2, currentPoint.Z) && !IsValidForGeneration(currentPoint.X, currentPoint.Z + 2) && !IsValidForGeneration(currentPoint.X, currentPoint.Z - 2))
                {
                    break;
                }
                
            }
        }
        
    }

    // 0 is N, 1 is E, 2 is S, 3 is W
    private int GenerateRandomDirection(int currentDirection)
    {
        if(UnityEngine.Random.value > 0.5)
        {
            return currentDirection;
        }
        else
        {
            return (int)((UnityEngine.Random.value * 4) % 4);
        }
    }

    public bool CanWalk(Vector3 positionToCheck)
    {
        // Adjust coordinates, as local coordinates of player is centred around the centre of the tile
        // TODO: Remember to fix positionToCheckZ to be Math.Floor when using real grid
        int y = (int)Math.Floor(positionToCheck.x);
        int x = (int)Math.Floor(positionToCheck.z);

        // Check if the grid was already visited as well
        return IsValid(x, y) && IsEmpty(x, y) && !labyrinthGrids[x,y].visited && !labyrinthGrids[x, y].isObstructed;
    }

    public LabyrinthGrid labyrinthLeftCornerGridPrefab;
    public LabyrinthGrid labyrinthRightCornerGridPrefab;
    public LabyrinthGrid labyrinthTreasureRoomGridPrefab;
    public LabyrinthGrid[] labyrinthGridPrefabs;
    public Obstacle obstaclePrefab;

    private void GenerateLabyrinth()
    {
        string waypoints = "";

        // Mark the directions (To identify direction of obstacle and corners)
        for(int i = 0; i < wayPoints.Count; i++)
        {
            int direction = 0;
            waypoints += wayPoints[i].point.X.ToString() + ',' + wayPoints[i].point.Z.ToString() + '\n';
            if(i > 0)
            {
                if(wayPoints[i].point.Z == wayPoints[i-1].point.Z + 1)
                {
                    direction = 0;                    
                }
                else if(wayPoints[i].point.Z == wayPoints[i - 1].point.Z - 1)
                {
                    direction = 2;
                }
                else if(wayPoints[i].point.X == wayPoints[i - 1].point.X + 1)
                {
                    direction = 1;
                }
                else if (wayPoints[i].point.X == wayPoints[i - 1].point.X - 1)
                {
                    direction = 3;
                }
                wayPoints[i - 1].toDirection = direction;
            }
            wayPoints[i].fromDirection = direction;



            /*labyrinthGrids[wayPoints[i].Z, wayPoints[i].X] = Instantiate(labyrinthGridPrefab, new Vector3(wayPoints[i].X + 0.5f, 1, wayPoints[i].Z + 0.5f), Quaternion.Euler(0, direction * 90, 0)) as LabyrinthGrid;
            labyrinthGrids[wayPoints[i].Z, wayPoints[i].X].direction = direction;
            if(i == 0)
            {
                // Visited the 1st waypoint, as the player starts from that waypoint
                labyrinthGrids[wayPoints[i].Z, wayPoints[i].X].visited = true;
            }*/
        }
        // Debug.Log(waypoints);

        // To generate the actual labyrinth
        for(int i = 0; i < wayPoints.Count; i++)
        {
            if(wayPoints[i].fromDirection != wayPoints[i].toDirection && wayPoints[i].toDirection != -1)
            {
                if(wayPoints[i].toDirection - wayPoints[i].fromDirection == 1 || wayPoints[i].toDirection - wayPoints[i].fromDirection == -3)
                {
                    labyrinthGrids[wayPoints[i].point.Z, wayPoints[i].point.X] = Instantiate(labyrinthRightCornerGridPrefab, new Vector3(wayPoints[i].point.X + 0.5f, 0, wayPoints[i].point.Z + 0.5f), Quaternion.Euler(0, wayPoints[i].fromDirection * 90, 0)) as LabyrinthGrid;

                }
                else if(wayPoints[i].fromDirection - wayPoints[i].toDirection == 1 || wayPoints[i].fromDirection - wayPoints[i].toDirection == -3)
                {
                    labyrinthGrids[wayPoints[i].point.Z, wayPoints[i].point.X] = Instantiate(labyrinthLeftCornerGridPrefab, new Vector3(wayPoints[i].point.X + 0.5f, 0, wayPoints[i].point.Z + 0.5f), Quaternion.Euler(0, wayPoints[i].fromDirection * 90, 0)) as LabyrinthGrid;
                }
            }
            else
            {
                if(i == wayPoints.Count - 1)
                {
                    labyrinthGrids[wayPoints[i].point.Z, wayPoints[i].point.X] = Instantiate(labyrinthTreasureRoomGridPrefab, new Vector3(wayPoints[i].point.X + 0.5f, 0, wayPoints[i].point.Z + 0.5f), Quaternion.Euler(0, wayPoints[i].fromDirection * 90, 0)) as LabyrinthGrid;
                }
                else
                {
                    labyrinthGrids[wayPoints[i].point.Z, wayPoints[i].point.X] = Instantiate(labyrinthGridPrefabs[UnityEngine.Random.Range(0, labyrinthGridPrefabs.Length)], new Vector3(wayPoints[i].point.X + 0.5f, 0, wayPoints[i].point.Z + 0.5f), Quaternion.Euler(0, wayPoints[i].fromDirection * 90, 0)) as LabyrinthGrid;
                }
            }
            if (i == 0)
            {
                // Visited the 1st waypoint, as the player starts from that waypoint
                labyrinthGrids[wayPoints[i].point.Z, wayPoints[i].point.X].visited = true;
            }
            // Testing obstacles
            if (i == 1)
            {
                labyrinthGrids[wayPoints[i].point.Z, wayPoints[i].point.X].obstacle = Instantiate(obstaclePrefab, new Vector3(wayPoints[i].point.X + 0.5f, 0, wayPoints[i].point.Z + 0.5f), Quaternion.Euler(0, wayPoints[i].fromDirection * 90, 0)) as Obstacle;
                labyrinthGrids[wayPoints[i].point.Z, wayPoints[i].point.X].isObstructed = true;
            }
        }
    }

    public void VisitedGrid(Vector3 positionToCheck)
    {
        int y = (int)Math.Floor(positionToCheck.x);
        int x = (int)Math.Floor(positionToCheck.z);

        labyrinthGrids[x, y].visited = true;
    }

    public void InteractWithLabyrinth(Vector3 positionToCheck, string instruction)
    {
        int y = (int)Math.Floor(positionToCheck.x);
        int x = (int)Math.Floor(positionToCheck.z);
        if (IsValid(x, y))
        {
            labyrinthGrids[x, y].InteractWithObstacle(instruction);
        }
        else
        {
            Debug.Log("Coordinates are invalid");
        }
    }
}
