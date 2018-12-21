using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabyrinthNavigation : MonoBehaviour {

    public char[,] labyrinthArray;

    public LabyrinthGrid[,] labyrinthGrids;

    public int pathLength = 8;

    // Wall prefab used to instantiate
    public Transform wallPrefab;
    // Floor prefab used to instantiate
    public Transform floorPrefab;

    public GridPoint startingPoint;

    List<GridPoint> wayPoints;
    // Use this for initialization
    void Start () {

        int length = UnityEngine.Random.Range(pathLength+5, pathLength + 10);
        int width = UnityEngine.Random.Range(pathLength+5, pathLength + 10);

        startingPoint = new GridPoint(0, 0);

        labyrinthArray = new char[width, length];
        labyrinthGrids = new LabyrinthGrid[width, length];

        GeneratePath();
        GenerateLabyrinth();

        /*string array = "";
        for(int i = 0; i < 7; i++)
        {
            for(int j = 0; j < 7; j++)
            {
                array += labyrinthArray[i, j];
            }
            array += '\n';
        }
        print(array);*/
        for (int i = 0; i < width; i++)
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
        }

        for (int i = -1; i <= width; i++)
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
        }

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
            currentPoint = new GridPoint(startingPoint.X, startingPoint.Y);
            labyrinthArray[currentPoint.X, currentPoint.Y] = 'o';
            wayPoints = new List<GridPoint>();
            wayPoints.Add(startingPoint);
            for (int i = 0; i < pathLength; i++)
            {
                bool blockGenerated = false;
                while (!blockGenerated)
                {
                    int direction = GenerateRandomDirection(currentDirection);
                    switch (direction)
                    {
                        case 0:
                            if (IsValidForGeneration(currentPoint.X, currentPoint.Y - 2))
                            {
                                labyrinthArray[currentPoint.X, currentPoint.Y - 1] = 'o';
                                labyrinthArray[currentPoint.X, currentPoint.Y - 2] = 'o';
                                
                                wayPoints.Add(new GridPoint(currentPoint.X, currentPoint.Y - 1));
                                wayPoints.Add(new GridPoint(currentPoint.X, currentPoint.Y - 2));

                                blockGenerated = true;
                                currentPoint.Y = currentPoint.Y - 2;
                                
                            }
                            break;
                        case 1:
                            if (IsValidForGeneration(currentPoint.X + 2, currentPoint.Y))
                            {
                                labyrinthArray[currentPoint.X + 1, currentPoint.Y] = 'o';
                                labyrinthArray[currentPoint.X + 2, currentPoint.Y] = 'o';

                                wayPoints.Add(new GridPoint(currentPoint.X + 1, currentPoint.Y));
                                wayPoints.Add(new GridPoint(currentPoint.X + 2, currentPoint.Y));

                                blockGenerated = true;
                                currentPoint.X = currentPoint.X + 2;
                            }
                            break;
                        case 2:
                            if (IsValidForGeneration(currentPoint.X, currentPoint.Y + 2))
                            {
                                labyrinthArray[currentPoint.X, currentPoint.Y + 1] = 'o';
                                labyrinthArray[currentPoint.X, currentPoint.Y + 2] = 'o';

                                wayPoints.Add(new GridPoint(currentPoint.X, currentPoint.Y + 1));
                                wayPoints.Add(new GridPoint(currentPoint.X, currentPoint.Y + 2));

                                blockGenerated = true;
                                currentPoint.Y = currentPoint.Y + 2;
                            }
                            break;
                        case 3:
                            if (IsValidForGeneration(currentPoint.X - 2, currentPoint.Y))
                            {
                                labyrinthArray[currentPoint.X - 1, currentPoint.Y] = 'o';
                                labyrinthArray[currentPoint.X - 2, currentPoint.Y] = 'o';

                                wayPoints.Add(new GridPoint(currentPoint.X - 1, currentPoint.Y));
                                wayPoints.Add(new GridPoint(currentPoint.X - 2, currentPoint.Y));

                                blockGenerated = true;
                                currentPoint.X = currentPoint.X - 2;
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
                if (!IsValidForGeneration(currentPoint.X, currentPoint.Y + 2) && !IsValidForGeneration(currentPoint.X - 2, currentPoint.Y) && !IsValidForGeneration(currentPoint.X, currentPoint.Y + 2) && !IsValidForGeneration(currentPoint.X, currentPoint.Y - 2))
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
        return IsValid(x, y) && IsEmpty(x, y);
    }

    public LabyrinthGrid labyrinthGridPrefab;

    private void GenerateLabyrinth()
    {
        //string waypoints = "";
        for(int i = 0; i < wayPoints.Count; i++)
        {
            
            //waypoints += wayPoints[i].X.ToString() + ',' + wayPoints[i].Y.ToString() + '\n';
            
            labyrinthGrids[wayPoints[i].X, wayPoints[i].Y] = Instantiate(labyrinthGridPrefab, new Vector3(wayPoints[i].Y, 1, wayPoints[i].X), Quaternion.identity) as LabyrinthGrid;
            if(i > 0)
            {
                if(wayPoints[i].X == wayPoints[i-1].X + 1)
                {
                    labyrinthGrids[wayPoints[i].X, wayPoints[i].Y].direction = 1;
                }
                else if(wayPoints[i].X == wayPoints[i - 1].X - 1)
                {
                    labyrinthGrids[wayPoints[i].X, wayPoints[i].Y].direction = 3;
                }
                else if(wayPoints[i].Y == wayPoints[i - 1].Y + 1)
                {
                    labyrinthGrids[wayPoints[i].X, wayPoints[i].Y].direction = 2;
                }
                else if (wayPoints[i].Y == wayPoints[i - 1].Y - 1)
                {
                    labyrinthGrids[wayPoints[i].X, wayPoints[i].Y].direction = 0;
                }
            }
        }
        //Debug.Log(waypoints);
    }
}
