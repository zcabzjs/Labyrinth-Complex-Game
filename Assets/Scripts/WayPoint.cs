using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour {

    public GridPoint point;
    public int fromDirection;
    public int toDirection = -1;
    
    public WayPoint(GridPoint point, int fromDirection, int toDirection)
    {
        this.point = point;
        this.fromDirection = fromDirection;
        this.toDirection = toDirection;
    }

    public WayPoint(GridPoint point)
    {
        this.point = point;
    }
}
