using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawn_UnitA : MonoBehaviour
{
    public GameObject unitToSpawn;

    public string pointName = "Cube_Button";

    public Vector3 spawnPosition;

    // Start is called before the first frame update
    void Start()
    {
        spawnPosition = GameObject.Find(pointName).transform.position;
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown (0))
        {
            Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            var myRaycast = Physics.Raycast(myRay, out hitInfo, 100);

            if (myRaycast && hitInfo.transform.gameObject.tag == "Spawn")
            {
                Debug.Log("Spawning...");
                Instantiate(unitToSpawn, spawnPosition, unitToSpawn.transform.rotation);
                unitToSpawn.name = ("Capsule" + Time.deltaTime);
            } 
        }
    }
}
