using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnUnit : MonoBehaviour
{
    public GameObject unitToSpawn;

    public Vector3 spawnPosition;

    public string name;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    public void Spawn()
    {
        Debug.Log("Spawning...");
        Instantiate(unitToSpawn, spawnPosition, unitToSpawn.transform.rotation);
        unitToSpawn.name = (name + Time.deltaTime);
    }
}
