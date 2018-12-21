﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPoint : MonoBehaviour {

    public int X { get; set; }
    public int Y { get; set; }

    public GridPoint(int x, int y)
    {
        this.X = x;
        this.Y = y;
    }
}
