using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using CodeMonkey;
using UnityEngine.EventSystems;

public class PathNode
{
    private GridXZ<PathNode> grid;
<<<<<<< Updated upstream
    private int y;
    private int x;
=======
    public int x;
    public int z;
>>>>>>> Stashed changes

    public int gCost;
    public int hCost;
    public int fCost;

    public PathNode cameFromNode;

<<<<<<< Updated upstream
    public PathNode(GridXZ<PathNode>, grid, int x, int y)
    {
        this.grid = grid;
        this.x = x;
        this.y = y;
=======
    public PathNode(GridXZ<PathNode> grid, int x, int z)
    {
        this.grid = grid;
        this.x = x;
        this.z = z;
>>>>>>> Stashed changes
    }

    public void CalculateFCost()
    {
        fCost = gCost + hCost;
    }

    public override string ToString()
    {
<<<<<<< Updated upstream
        return x + "," + y;
=======
        return x + "," + z;
>>>>>>> Stashed changes
    }
}
