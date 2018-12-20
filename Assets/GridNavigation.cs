using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridNavigation : MonoBehaviour {
    public class GameGrid
    {
        public int length;
        public int width;
        public Transform[,] gridArray;
        public Transform floorPrefab;
        public Transform wallPrefab;
        public Transform outerWallPrefab;

        public GameGrid(int length, int width, Transform floorPrefab, Transform wallPrefab, Transform outerWallPrefab)
        {
            this.length = length;
            this.width = width;
            this.gridArray = new Transform[width, length];

            for (int i = 0; i < width - 1; i++)
            {
                for (int j = 0; j < length - 1; j++)
                {
                    Instantiate(floorPrefab, new Vector3(j, 0, i), Quaternion.identity);

                    //About 70% filled
                    bool result = UnityEngine.Random.value > 0.7;
                    if (result)
                    {

                        gridArray[i, j] = Instantiate(wallPrefab, new Vector3(j, 1, i), Quaternion.identity);
                    }
                }
            }

            // For instantiating the outer-wall surrounding the grid
            for (int i = -1; i < width; i++)
            {
                for (int j = -1; j < length; j++)
                {
                    if (i == -1 || i == width - 1)
                    {
                        Instantiate(outerWallPrefab, new Vector3(j, 1, i), Quaternion.identity);
                    }
                    else if (j == -1 || j == length - 1)
                    {
                        Instantiate(outerWallPrefab, new Vector3(j, 1, i), Quaternion.identity);
                    }
                }
            }

        }
    }

    public static GameGrid gameGrid;

    // wall prefab used for the grid
    public Transform wallPrefab;

    // floor prefab used for the grid
    public Transform floorPrefab;

    // outer-wall prefab used for the grid
    public Transform outerWallPrefab;

    void Start()
    {
        // Initialising grid array with our current fixed array
        /*gridArray = new int[10, 10] {
            {1,1,1,1,1,1,1,1,1,1},
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,1,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,1,1,0},
            {0,0,1,1,1,1,0,1,1,0},
            {0,0,0,0,0,1,0,1,1,0},
            {1,0,0,0,0,1,0,0,0,0},
            {1,1,0,0,0,1,0,0,0,0},
            {1,0,0,0,0,1,1,1,1,1},
            {1,1,1,0,0,0,0,0,0,0}
        };*/


        int length = UnityEngine.Random.Range(8, 20);
        int width = UnityEngine.Random.Range(8, 20);
        gameGrid = new GameGrid(length, width, floorPrefab, wallPrefab, outerWallPrefab);
        
    }
    /*
        If array is [3][4], this means its:
        o o o o
        o o o o
        o o o o

        Z coordinates represent the 3, while X coordinates represent the 4
    */


    // Takes in coordinates and checks grid to see if theres anything there
    public static bool IsOccupied(int x, int y)
    {
        return (gameGrid.gridArray[x, y] != null);
    }

    static Transform GetTransformInGrid(int x, int y)
    {
        return gameGrid.gridArray[x, y];
    }

    private static bool ValidatePointOnGrid(int x, int z)
    {
        if (x < 0 || z < 0)
        {
            return false;
        }
        else if (x >= gameGrid.length - 1 || z >= gameGrid.width - 1)
        {
            return false;
        }
        return true;
    }

    public static bool CheckIfCanWalk(Vector3 positionToCheck)
    {
        // Adjust coordinates, as local coordinates of player is centred around the centre of the tile
        // TODO: Remember to fix positionToCheckZ to be Math.Floor when using real grid
        int positionToCheckX = (int)Math.Floor(positionToCheck.x);
        int positionToCheckZ = (int)Math.Floor(positionToCheck.z);

        // Debug.Log(positionToCheckX);
        // Debug.Log(positionToCheckZ);

        // Check if new position is outside of grid
        if (!ValidatePointOnGrid(positionToCheckX, positionToCheckZ))
        {
            return false;
        }

        // Check if new position is occupied in grid
        if (IsOccupied(positionToCheckZ, positionToCheckX))
        {
            return false;
        }

        return true;
    }


}
