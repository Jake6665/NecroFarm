using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseSnap : MonoBehaviour
{

    private GameObject heldObject;
    //prefab to be placed
    [SerializeField]
    private GameObject spawnObject;
    private Grid grid;
    bool createMode = false;
    private void Awake()
    {
        grid = FindObjectOfType<Grid>();
    }
    //called by a "Create" button or whatever
    public void onCreateButton()
    {
        SpawnAndHoldThing(spawnObject);
    }


    public void SpawnAndHoldThing(GameObject thingPrefab)
    {
        heldObject = Instantiate(thingPrefab) as GameObject;

    }

    void Update()
    {
        RaycastHit hitInfo;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetKeyDown(KeyCode.C))
        {
            if(createMode == false)
            {
                onCreateButton();
            }

            createMode = true;
        }

        if (Physics.Raycast(ray, out hitInfo))
        {
            if (heldObject)
            {
                heldObject.transform.position = grid.GetNearestPointOnGrid(hitInfo.point);
            }
        }


    }


}
