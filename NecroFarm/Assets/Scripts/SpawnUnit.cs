using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnUnit : MonoBehaviour
{
    public GameObject unitToSpawn;

    public Vector3 spawnPosition;

    public string name;

    private bool timer;

    public float waitTime;

    [SerializeField]
    private GameObject gameManager;

    // Start is called before the first frame update
    void Start()
    {
        timer = true;
    }

    IEnumerator waiting()
    {
        Debug.Log("Started Waiting at: " + Time.time);
        yield return new WaitForSeconds(waitTime);
        Debug.Log("Stopped Waiting at: " + Time.time);
        timer = true;
    }

    public void Spawn()
    {
        if (gameManager.GetComponent<playerEconomy>().enoughBones())
        {
            if (timer)
            {
                Debug.Log("Spawning...");
                Instantiate(unitToSpawn, spawnPosition, unitToSpawn.transform.rotation);
                unitToSpawn.name = (name + Time.deltaTime);
                gameManager.GetComponent<playerEconomy>().subtractBones();
                timer = false;
            }

            if (!timer)
            {
                Debug.Log("Waiting...");
                StartCoroutine(waiting());
            }

        }

    }
}
