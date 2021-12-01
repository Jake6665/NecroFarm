using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using UnityEngine.EventSystems;

public class GridBuildingSystem : MonoBehaviour {
    public static GridBuildingSystem Instance { get; private set; }
    public GridXZ<GridObject> grid;
    [SerializeField]
    private List<ScriptableObjects> scriptableObjectList;
    private ScriptableObjects scriptableObject;
    private ScriptableObjects.Dir dir = ScriptableObjects.Dir.Down;

    [SerializeField]
    private GameObject gameManager;
    [SerializeField]
    private List<GameObject> ghostObjectList;
    private GameObject ghostObject;
    private GameObject heldGhost;
    private String ghostName = null;
    private Vector3 ghostPOS;
    private float ghostCellSize;


    //Game States
        //If crop UI is open
    private bool plantingState = false;
        //If displaying the buy plot grid
    private bool plotState = false;

    private void Awake()
    {
        Instance = this;
        //Total grid size
        int gridWidth = 1500;
        int gridHeight = 1500;

        //Individual cell size
        float cellSize = 2f;
        ghostCellSize = cellSize;
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
        if (plantingState && !plotState)
        {
            if (GameObject.Find(ghostName))
            {
                Vector3 ghostPOS = Mouse3D.GetMouseWorldPosition();
                grid.GetXZ(ghostPOS, out int x, out int z);
                Vector3 ghostdPosition = grid.GetWorldPosition(x, z);
                ghostdPosition.x += ghostCellSize / 2;
                ghostdPosition.z += ghostCellSize / 2;
                heldGhost.transform.position = ghostdPosition;
            }
            //Place Object on gird
            if (Input.GetMouseButtonDown(0) && scriptableObject != null)
            {
                //Check if player Clicked On UI
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    return;
                }
                //Make sure planint in soil
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f))
                {
                    if(raycastHit.transform.tag == "Well")
                    {
                        //Check if player can afford
                        if (gameManager.GetComponent<playerEconomy>().canAfford(scriptableObject.buyPrice))
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

                            //Check location to see if on grid is occupied
                            if (canBuild)
                            {
                                Vector2Int rotationOffest = scriptableObject.GetRotationOffset(dir);
                                Vector3 scriptableWorldPosition = grid.GetWorldPosition(x, z) + new Vector3(rotationOffest.x, 0, rotationOffest.y) * grid.GetCellSize();
                                PlacedObject placedObject = PlacedObject.Create(scriptableWorldPosition, new Vector2Int(x, z), dir, scriptableObject);
                                //Handle mutli-tiled claims from objects
                                foreach (Vector2Int gridPosition in gridPositionList)
                                {
                                    grid.GetGridObject(gridPosition.x, gridPosition.y).SetPlacedObject(placedObject);
                                }

                                gameManager.GetComponent<playerEconomy>().subtractFunds(scriptableObject.buyPrice);
                            }//Gird tile is occupied
                            else
                            {
                                UtilsClass.CreateWorldTextPopup("Cannot build here!", Mouse3D.GetMouseWorldPosition());
                            }
                        }//Not enough funds to purchase
                        else
                        {
                            UtilsClass.CreateWorldTextPopup("Cannot afford this!", Mouse3D.GetMouseWorldPosition());
                        }

                    }


                }
            }

            //Sell grown crops
            if (Input.GetMouseButtonDown(1))
            {
                //Clear Object from grid
                GridObject gridObject = grid.GetGridObject(Mouse3D.GetMouseWorldPosition());
                PlacedObject placedObject = gridObject.GetPlacedObject();
                if (placedObject != null)
                {
                    //Check if plant is grown
                    if (placedObject.transform.GetComponent<cropGrowth>().isGrown())
                    {
                        //Add funds based on sell price
                        gameManager.GetComponent<playerEconomy>().addFunds(placedObject.GetScriptableRefrence().sellPrice);
                        //Clear Object from grid
                        placedObject.DestroySelf();
                        //Also clear any other grid claims the object may have
                        List<Vector2Int> gridPositionList = placedObject.GetGridPositionList();
                        foreach (Vector2Int gridPosition in gridPositionList)
                        {
                            grid.GetGridObject(gridPosition.x, gridPosition.y).ClearObject();
                        }
                    }
                }
            }



            //Rotate Object
            if (Input.GetKeyDown(KeyCode.R))
            {
                dir = ScriptableObjects.GetNextDir(dir);
            }


            //Hotkeys (Can be removed but option in addition to buttons)
            //NOTE ONLY WORKS IN PLANTING STATE
            if (Input.GetKeyDown(KeyCode.Alpha1)) { scriptableObject = scriptableObjectList[0]; }
            if (Input.GetKeyDown(KeyCode.Alpha2)) { scriptableObject = scriptableObjectList[1]; }
            if (Input.GetKeyDown(KeyCode.Alpha3)) { scriptableObject = scriptableObjectList[2]; }

        }
        else
        {
            if (GameObject.Find(ghostName))
            {
                Destroy(heldGhost);
            }
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

    public void TogglePlot()
    {
        scriptableObject = null;
        if (plotState == true)
        {
            plotState = false;
        }
        else
        {
            plotState = true;
        }

    }

    public void DisablePlot()
    {
        plotState = false;
    }

    public bool CheckPlotState()
    {
        return plotState;
    }

    public void ChangeGhostObject(int i )
    {
        Destroy(heldGhost);
        ghostObject = ghostObjectList[i];
        heldGhost = Instantiate(ghostObject, Mouse3D.GetMouseWorldPosition(), Quaternion.identity);
        ghostName = heldGhost.name;
    }


    //Upon loading adds object back into the grid
    public void RestoreToGrid(GameObject obj)
    {
        Debug.Log("Running restore");

        
        scriptableObject = obj.GetComponent<cropGrowth>().selfRef;
        
        {

            {
                Vector3 mousePosition = new Vector3(obj.transform.position.x, 0, obj.transform.position.z);
                grid.GetXZ(mousePosition, out int x, out int z);
                Vector2Int placedObjectOrigin = new Vector2Int(x, z);
                placedObjectOrigin = grid.ValidateGridPosition(placedObjectOrigin);

                List<Vector2Int> gridPositionList = scriptableObject.GetGridPositionList(new Vector2Int(x, z), dir);
                {
                    Vector2Int rotationOffest = scriptableObject.GetRotationOffset(dir);
                    Vector3 scriptableWorldPosition2 = grid.GetWorldPosition(x, z) + new Vector3(rotationOffest.x, 0, rotationOffest.y) * grid.GetCellSize();
                    PlacedObject placedObject2 = PlacedObject.Create(scriptableWorldPosition2, new Vector2Int(x, z), dir, scriptableObject);
                    //Handle mutli-tiled claims from objects
                    foreach (Vector2Int gridPosition in gridPositionList)
                    {
                        grid.GetGridObject(gridPosition.x, gridPosition.y).SetPlacedObject(placedObject2);
                    }
                }
            }

        }
   
    }

}
