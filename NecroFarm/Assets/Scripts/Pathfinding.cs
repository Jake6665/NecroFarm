using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using CodeMonkey;
using UnityEngine.EventSystems;

public class Pathfinding
{
    private const int MOVE_STRAIGHT_COST = 10;
    private const int MOVE_DIAGONAL_COST = 14;

    private GridXZ<PathNode> grid;
    private List<PathNode> openList;
<<<<<<< Updated upstream
    private List<Pathnode> closedList;

    public Pathfinding(int width, int height)
    {
        grid = new GridXZ<PathNode>(width, height, 10f, Vector3.zero, (GridXZ<PathNode> grid, int x, int y) => new PathNode(g, x, y));
    }

    public Grid<PathNode> GetGrid()
=======
    private List<PathNode> closedList;

    public Pathfinding(int width, int height)
    {
        grid = new GridXZ<PathNode>(width, height, 10f, Vector3.zero, (GridXZ<PathNode> g, int x, int z) => new PathNode(g, x, z));
    }

    public GridXZ<PathNode> GetGrid()
>>>>>>> Stashed changes
    {
        return grid;
    }

<<<<<<< Updated upstream
    public List<PathNode> FindPath(int startX, int startY, int endX, int endY)
    {
        PathNode startNode = grid.GetGridObject(startX, startY);
        PathNode endNode = grid.GetGridObject(endX, endY);

        openList = new List<PathNode> { startnode };
        closedList = new List<Pathnode>();

        for (int x = 0; x < grid.GetWidth(); x++)
        {
            for (int y = 0; y < grid.GetHeight(); y++)
            {
                PathNode pathNode = grid.GetGridObject(x, y);
=======
    public List<PathNode> FindPath(int startX, int startZ, int endX, int endZ)
    {
        PathNode startNode = grid.GetGridObject(startX, startZ);
        PathNode endNode = grid.GetGridObject(endX, endZ);

        openList = new List<PathNode> { startNode };
        closedList = new List<PathNode>();

        for (int x = 0; x < grid.GetWidth(); x++)
        {
            for (int z = 0; z < grid.GetHeight(); z++)
            {
                PathNode pathNode = grid.GetGridObject(x, z);
>>>>>>> Stashed changes

                pathNode.gCost = int.MaxValue;
                pathNode.CalculateFCost();
                pathNode.cameFromNode = null;
            }
        }

        startNode.gCost = 0;
        startNode.hCost = CalculateDistanceCost(startNode, endNode);
        startNode.CalculateFCost();


        while(openList.Count > 0)
        {
            PathNode currentNode = GetLowestFCostNode(openList);

            if (currentNode == endNode)
            {
                return CalculatePath(endNode);
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            foreach (PathNode neighborNode in GetNeighborList(currentNode))
            {
                if (closedList.Contains(neighborNode)) continue;

                int tentativeGCost = currentNode.gCost + CalculateDistanceCost(currentNode, neighborNode);

                if (tentativeGCost < neighborNode.gCost)
                {
                    neighborNode.cameFromNode = currentNode;
                    neighborNode.gCost = tentativeGCost;
                    neighborNode.hCost = CalculateDistanceCost(neighborNode, endNode);
                    neighborNode.CalculateFCost();

                    if (!openList.Contains(neighborNode))
                    {
                        openList.Add(neighborNode);
                    }
                }
            }
        }

        // out of nodes on open list
        return null;
    }

<<<<<<< Updated upstream

=======
>>>>>>> Stashed changes
    private List<PathNode> GetNeighborList(PathNode currentNode)
    {
        List<PathNode> neighborList = new List<PathNode>();

        if (currentNode.x - 1 >= 0)
        {
            // Left
<<<<<<< Updated upstream
            neighborList.Add(GetNode(currentNode.x - 1, currentNode.y));

            // Left Down
            if (currentNode.y - 1 >= 0) { neighborList.Add(GetNode(currentNode.x - 1, currentNode.y - 1)); }

            // Left Up
            if (currentNode.y + 1 < grid.GetHeight()) { neighborList.Add(GetNode(currentNode.x - 1, currentNode.y + 1)); }
=======
            neighborList.Add(GetNode(currentNode.x - 1, currentNode.z));

            // Left Down
            if (currentNode.z - 1 >= 0) { neighborList.Add(GetNode(currentNode.x - 1, currentNode.z - 1)); }

            // Left Up
            if (currentNode.z + 1 < grid.GetHeight()) { neighborList.Add(GetNode(currentNode.x - 1, currentNode.z + 1)); }
>>>>>>> Stashed changes

        }

        if (currentNode.x + 1 < grid.GetWidth())
        {
            // Right
<<<<<<< Updated upstream
            neighborList.Add(GetNode(currentNode.x + 1, currentNode.y));

            // Right Down
            if (currentNode.y - 1 >= 0) { neighborList.Add(GetNode(currentNode.x + 1, currentNode.y - 1)); }

            // Right Up
            if (currentNode.y + 1 < grid.GetHeight()) { neighborList.Add(GetNode(currentNode.x + 1, currentNode.y + 1)); }
=======
            neighborList.Add(GetNode(currentNode.x + 1, currentNode.z));

            // Right Down
            if (currentNode.z - 1 >= 0) { neighborList.Add(GetNode(currentNode.x + 1, currentNode.z - 1)); }

            // Right Up
            if (currentNode.z + 1 < grid.GetHeight()) { neighborList.Add(GetNode(currentNode.x + 1, currentNode.z + 1)); }
>>>>>>> Stashed changes

        }

        // Down
<<<<<<< Updated upstream
        if (currentNode.y - 1 >= 0) { neighborList.Add(GetNode(currentNode.x, currentNode.y - 1)); }

        // Up
        if (currentNode.y + 1 < grid.GetHeight()) { neighborList.Add(GetNode(currentNode.x, currentNode.y + 1)); }
=======
        if (currentNode.z - 1 >= 0) { neighborList.Add(GetNode(currentNode.x, currentNode.z - 1)); }

        // Up
        if (currentNode.z + 1 < grid.GetHeight()) { neighborList.Add(GetNode(currentNode.x, currentNode.z + 1)); }
>>>>>>> Stashed changes

        return neighborList;
    }

<<<<<<< Updated upstream
=======
    private PathNode GetNode(int x, int z)
    {
        return grid.GetGridObject(x, z);
    }

>>>>>>> Stashed changes
    private List<PathNode> CalculatePath(PathNode endNode)
    {
        List<PathNode> path = new List<PathNode>();

        path.Add(endNode);
        PathNode currentNode = endNode;

        while (currentNode.cameFromNode != null)
        {
            path.Add(currentNode.cameFromNode);
            currentNode = currentNode.cameFromNode;
        }

        path.Reverse();

        return path;
    }

<<<<<<< Updated upstream
    private int CalculateDistanceCost(PNode a, PNode b)
    {
        int xDistance = Mathf.Abs(a.x - b.x);
        int yDistance = Mathf.Abs(a.y - b.y);
        int remaining = Mathf.Abs(xDistance - yDistance);

        return MOVE_DIAGONAL_COST * Mathf.Min(xDistance, yDistance) + MOVE_STRAIGHT_COST * remaining;
=======
    private int CalculateDistanceCost(PathNode a, PathNode b)
    {
        int xDistance = Mathf.Abs(a.x - b.x);
        int zDistance = Mathf.Abs(a.z - b.z);
        int remaining = Mathf.Abs(xDistance - zDistance);

        return MOVE_DIAGONAL_COST * Mathf.Min(xDistance, zDistance) + MOVE_STRAIGHT_COST * remaining;
>>>>>>> Stashed changes
    }

    private PathNode GetLowestFCostNode(List<PathNode> pathNodeList)
    {
<<<<<<< Updated upstream
        pathNodeList lowestFCostNode = pathNodeList[0];
=======
        PathNode lowestFCostNode = pathNodeList[0];
>>>>>>> Stashed changes

        for (int i = 1; i < pathNodeList.Count; i++)
        {
            if (pathNodeList[i].fCost < lowestFCostNode.fCost)
            {
                lowestFCostNode = pathNodeList[i];
            }
        }

        return lowestFCostNode;
    }

}
