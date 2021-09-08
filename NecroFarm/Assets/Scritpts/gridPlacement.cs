using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gridPlacement : MonoBehaviour
{
    private Grid grid;
    [SerializeField]
    private GameObject spawnObject;
    bool isPlaced = false;
    private void Awake()
    {
        grid = FindObjectOfType<Grid>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            RaycastHit hitInfo;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hitInfo))
            {
                PlaceNear(hitInfo.point);
            }
        }
    }

    private void PlaceNear(Vector3 clickPoint)
    {
        if(isPlaced == false)
        {
            var finalPosition = grid.GetNearestPointOnGrid(clickPoint);
            //GameObject.CreatePrimitive(PrimitiveType.Cube).transform.position = finalPosition;
            GameObject.Instantiate(spawnObject).transform.position = finalPosition;
            isPlaced = true;
        }
    
    }
}
