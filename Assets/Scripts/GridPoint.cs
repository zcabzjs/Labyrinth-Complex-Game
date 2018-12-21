using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPoint : MonoBehaviour {

    public int X { get; set; }
    public int Z { get; set; }

    public GridPoint(int x, int z)
    {
        this.X = x;
        this.Z = z;
    }
}
