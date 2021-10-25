using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnUnit : MonoBehaviour
{
    public GameObject unitToSpawn;

    public string pointName = "Cube_Button";

    public Vector3 spawnPosition;

    // Start is called before the first frame update
    void Start()
    {
        spawnPosition = GameObject.Find(pointName).transform.position;
    }

    // Update is called once per frame
    public void Spawn()
    {
        Debug.Log("Spawning...");
        Instantiate(unitToSpawn, spawnPosition, unitToSpawn.transform.rotation);
        unitToSpawn.name = ("Capsule" + Time.deltaTime);
    }
}
