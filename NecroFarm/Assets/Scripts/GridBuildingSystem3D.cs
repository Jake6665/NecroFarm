using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class GridBuildingSystem3D : MonoBehaviour {

    private GridXZ<GridObject> grid;
    [SerializeField] private Transform testTransform;

    private void Awake()
    {
        int gridWidth = 20;
        int gridHeight = 20;
        float cellSize = 2f;
        grid = new GridXZ<GridObject>(gridWidth, gridHeight, cellSize, Vector3.zero, (GridXZ<GridObject> g, int x, int z) => new GridObject(g,x,z));
    }

    public class GridObject
    {
        private GridXZ<GridObject> grid;
        private int x;
        private int z;
        private Transform transform;

        public GridObject(GridXZ<GridObject> grid, int x, int z)
        {
            this.grid = grid;
            this.x = x;
            this.z = z;

        }

        public void SetTransform(Transform t)
        {
            this.transform = t;
            grid.TriggerGridObjectChanged(x, z);
        }

        public void ClearTransform()
        {
            transform = null;
        }

        public override string ToString()
        {
            return x + ", " + z +"\n" + transform;
        }

        public bool CanBuild()
        {
            return transform == null;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            RaycastHit hitInfo;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hitInfo))
            {
                grid.GetXZ(Mouse3D.GetMouseWorldPosition(), out int x, out int z);

                GridObject gridObject =grid.GetGridObject(x, z);
                if (gridObject.CanBuild())
                {
                    Transform builtTransform = Instantiate(testTransform, grid.GetWorldPosition(x, z), Quaternion.identity);
                    gridObject.SetTransform(builtTransform);

                }
                else
                {
                    UtilsClass.CreateWorldTextPopup("Cannot build here!", Mouse3D.GetMouseWorldPosition());
                }

            }

        }
    }


}
