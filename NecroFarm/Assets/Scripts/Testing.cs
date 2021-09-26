using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using CodeMonkey;

public class Testing : MonoBehaviour
{
<<<<<<< Updated upstream
    private Pathfinding pathfinding;
=======
    public Pathfinding pathfinding;
>>>>>>> Stashed changes

    // Start is called before the first frame update
    private void Start()
    {
<<<<<<< Updated upstream
        pathfinding = new PathFinding(10, 10);
=======
        pathfinding = new Pathfinding(10, 10);
>>>>>>> Stashed changes
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();
<<<<<<< Updated upstream
            pathfinding.GetGrid().GetXY(mouseWorldPosition, out int x, out int y);
            List<PathNode> path = pathfinding.FindPath(0, 0, x, y);
=======
            pathfinding.GetGrid().GetXZ(mouseWorldPosition, out int x, out int z);
            List<PathNode> path = pathfinding.FindPath(0, 0, x, z);
>>>>>>> Stashed changes

            if (path != null)
            {
                for (int i = 0; i < path.Count - 1; i++)
                {
<<<<<<< Updated upstream
                    Debug.Drawline(new Vector3(path[i].x, path[i].y) + 10f + Vector3.one * 5f, new Vector3(path[i+1].x, path[i+1].y) * 10f + Vector3.one * 5f, Color.green);
=======
                    Debug.DrawLine(new Vector3(path[i].x, path[i].z) * 10f + Vector3.one * 5f, new Vector3(path[i+1].x, path[i+1].z) * 10f + Vector3.one * 5f, Color.green);
>>>>>>> Stashed changes
                }
            }
        }
    }
}
