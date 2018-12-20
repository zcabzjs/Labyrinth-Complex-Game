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

    // Use this for initialization
    void Start () {

        int length = UnityEngine.Random.Range(8, 12);
        int width = UnityEngine.Random.Range(8, 12);


        labyrinthArray = new char[width, length];
        for(int i = 0; i < width; i++)
        {
            for(int j = 0; j < length; j++)
            {
                labyrinthArray[i, j] = 'x';
            }
        }

        GeneratePath();

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
        for(int i = 0; i < width; i++)
        {
            for (int j = 0; j < length; j++)
            {
                Instantiate(floorPrefab, new Vector3(j, 0, i), Quaternion.identity);
                if(labyrinthArray[i,j] == 'x')
                {
                    Instantiate(wallPrefab, new Vector3(j, 1, i), Quaternion.identity);

                }
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
        int[] currentPoint = new int[2] { 0, 0 };
        int currentDirection = 2;
        labyrinthArray[currentPoint[0], currentPoint[1]] = 'o';
        for(int i = 0; i < pathLength; i++)
        {
            bool blockGenerated = false;
            while (!blockGenerated)
            {
                int direction = GenerateRandomDirection(currentDirection);
                switch (direction)
                {
                    case 0: 
                        if(IsValidForGeneration(currentPoint[0], currentPoint[1] - 2))
                        {
                            labyrinthArray[currentPoint[0], currentPoint[1] - 2] = 'o';
                            labyrinthArray[currentPoint[0], currentPoint[1] - 1] = 'o';
                            blockGenerated = true;
                            currentPoint[1] = currentPoint[1] - 2;
                        }
                        break;
                    case 1:
                        if (IsValidForGeneration(currentPoint[0] + 2, currentPoint[1]))
                        {
                            labyrinthArray[currentPoint[0] + 2, currentPoint[1]] = 'o';
                            labyrinthArray[currentPoint[0] + 1, currentPoint[1]] = 'o';
                            blockGenerated = true;
                            currentPoint[0] = currentPoint[0] + 2;
                        }
                        break;
                    case 2:
                        if (IsValidForGeneration(currentPoint[0], currentPoint[1] + 2))
                        {
                            labyrinthArray[currentPoint[0], currentPoint[1] + 2] = 'o';
                            labyrinthArray[currentPoint[0], currentPoint[1] + 1] = 'o';
                            blockGenerated = true;
                            currentPoint[1] = currentPoint[1] + 2;
                        }
                        break;
                    case 3:
                        if (IsValidForGeneration(currentPoint[0] - 2, currentPoint[1]))
                        {
                            labyrinthArray[currentPoint[0] - 2, currentPoint[1]] = 'o';
                            labyrinthArray[currentPoint[0] - 1, currentPoint[1]] = 'o';
                            blockGenerated = true;
                            currentPoint[0] = currentPoint[0] - 2;
                        }
                        break;
                    default:
                        Debug.Log("Something wrong with the directions during path generation.");
                        break;
                }
            }
        }
    }

    // 0 is N, 1 is E, 2 is S, 3 is W
    private int GenerateRandomDirection(int currentDirection)
    {
        if(UnityEngine.Random.value > 0.9)
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
}
