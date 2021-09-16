﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class GridBuildingSystem : MonoBehaviour {
    public static GridBuildingSystem Instance { get; private set; }
    private GridXZ<GridObject> grid;
    [SerializeField]
    private List<ScriptableObjects> scriptableObjectList;
    private ScriptableObjects scriptableObject;
    private ScriptableObjects.Dir dir = ScriptableObjects.Dir.Down;
    private bool plantingState = false;
    private void Awake()
    {
        Instance = this;
        int gridWidth = 20;
        int gridHeight = 20;
        float cellSize = 2f;
        grid = new GridXZ<GridObject>(gridWidth, gridHeight, cellSize, Vector3.zero, (GridXZ<GridObject> g, int x, int z) => new GridObject(g,x,z));

        scriptableObject = null;
    }

    public class GridObject
    {
        private GridXZ<GridObject> grid;
        private int x;
        private int z;
        private PlacedObject placedObject;

        public GridObject(GridXZ<GridObject> grid, int x, int z)
        {
            this.grid = grid;
            this.x = x;
            this.z = z;

        }

        public void SetPlacedObject(PlacedObject pO)
        {
            this.placedObject = pO;
            grid.TriggerGridObjectChanged(x, z);
        }

        public void ClearObject()
        {
            placedObject = null;
            grid.TriggerGridObjectChanged(x, z);
        }

        public override string ToString()
        {
            return x + ", " + z +"\n" + placedObject;
        }

        public bool CanBuild()
        {
            return placedObject == null;
        }

        public PlacedObject GetPlacedObject()
        {
            return placedObject;
        }
    }

    private void Update()
    {
        if (plantingState)
        {
            //Place Object on gird
            if (Input.GetMouseButtonDown(0) && scriptableObject != null)
            {
                Vector3 mousePosition = Mouse3D.GetMouseWorldPosition();
                grid.GetXZ(mousePosition, out int x, out int z);
                Vector2Int placedObjectOrigin = new Vector2Int(x, z);
                placedObjectOrigin = grid.ValidateGridPosition(placedObjectOrigin);

                List<Vector2Int> gridPositionList = scriptableObject.GetGridPositionList(new Vector2Int(x, z), dir);
                bool canBuild = true;

                foreach (Vector2Int gridPosition in gridPositionList)
                {
                    if (!grid.GetGridObject(gridPosition.x, gridPosition.y).CanBuild())
                    {
                        canBuild = false;
                        break;
                    }
                }

                if (canBuild)
                {
                    Vector2Int rotationOffest = scriptableObject.GetRotationOffset(dir);
                    Vector3 scriptableWorldPosition = grid.GetWorldPosition(x, z) + new Vector3(rotationOffest.x, 0, rotationOffest.y) * grid.GetCellSize();
                    PlacedObject placedObject = PlacedObject.Create(scriptableWorldPosition, new Vector2Int(x, z), dir, scriptableObject);
                    foreach (Vector2Int gridPosition in gridPositionList)
                    {
                        grid.GetGridObject(gridPosition.x, gridPosition.y).SetPlacedObject(placedObject);
                    }
                }
                else
                {
                    UtilsClass.CreateWorldTextPopup("Cannot build here!", Mouse3D.GetMouseWorldPosition());
                }


            }
            //Clear Object from grid
            if (Input.GetMouseButtonDown(1))
            {
                GridObject gridObject = grid.GetGridObject(Mouse3D.GetMouseWorldPosition());
                PlacedObject placedObject = gridObject.GetPlacedObject();
                if (placedObject != null)
                {
                    placedObject.DestroySelf();

                    List<Vector2Int> gridPositionList = placedObject.GetGridPositionList();
                    foreach (Vector2Int gridPosition in gridPositionList)
                    {
                        grid.GetGridObject(gridPosition.x, gridPosition.y).ClearObject();
                    }

                }
            }



            //Rotate Object
            if (Input.GetKeyDown(KeyCode.R))
            {
                dir = ScriptableObjects.GetNextDir(dir);
            }


            //Hotkeys
            if (Input.GetKeyDown(KeyCode.Alpha1)) { scriptableObject = scriptableObjectList[0]; }
            if (Input.GetKeyDown(KeyCode.Alpha2)) { scriptableObject = scriptableObjectList[1]; }
            if (Input.GetKeyDown(KeyCode.Alpha3)) { scriptableObject = scriptableObjectList[2]; }

        }

    }

    public Vector2Int GetGridPosition(Vector3 worldPosition) {
        grid.GetXZ(worldPosition, out int x, out int z);
        return new Vector2Int(x, z);
    }

    public Vector3 GetMouseWorldSnappedPosition() {
        Vector3 mousePosition = Mouse3D.GetMouseWorldPosition();
        grid.GetXZ(mousePosition, out int x, out int z);

        if (scriptableObject != null) {
            Vector2Int rotationOffset = scriptableObject.GetRotationOffset(dir);
            Vector3 placedObjectWorldPosition = grid.GetWorldPosition(x, z) + new Vector3(rotationOffset.x, 0, rotationOffset.y) * grid.GetCellSize();
            return placedObjectWorldPosition;
        } else {
            return mousePosition;
        }
    }

    public Quaternion GetScriptableObjectRotation() {
        if (scriptableObject != null) {
            return Quaternion.Euler(0, scriptableObject.GetRotationAngle(dir), 0);
        } else {
            return Quaternion.identity;
        }
    }

    public ScriptableObjects GetScriptableObject() {
        return scriptableObject;
    }

    public void SetObject(int i)
    {
        scriptableObject = scriptableObjectList[i];
    }

    public void TogglePlanting()
    {
        scriptableObject = null;
        if (plantingState == true)
        {
            plantingState = false;
        }
        else
        {
            plantingState = true;
        }

    }

}